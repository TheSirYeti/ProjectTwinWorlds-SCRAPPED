using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovableItem : InteractableObject
{
    public Transform itemToFollow;
    public Transform itemToRestrict;
    public float minDistance, speed, velocity;
    public Rigidbody rb;
    public bool isRestricting, isFollowing, isSwinging, isFalling;
    public Transform jumpSpot;

    public Transform target;
    public SwingPhysics mySwing;
    public LineRenderer lineRenderer;

    public override void OnObjectStart()
    {
        EventManager.UnSubscribe("OnPlayerChange", ChangeMovingMode);
        EventManager.Subscribe("OnPlayerChange", ChangeMovingMode);
        
        EventManager.Subscribe("ResetAbility", OnItemCanceled);

        if (mySwing != null)
        {
            CutSwingTies(null);
            transform.position = itemToFollow.transform.position;
        }

        lineRenderer.enabled = true;
        target = itemToFollow;
        isFollowing = true;
        isRestricting = false;
        
        isObjectTriggered = true;
    }

    public override void OnObjectDuring()
    {
        if (isFollowing)
        {
            if (Vector3.Distance(itemToFollow.position, transform.position) >= minDistance)
            {
                Vector3 direction = itemToFollow.position - transform.position;

                if (direction.magnitude > 1)
                {
                    direction.Normalize();
                }

                rb.MovePosition(transform.position += direction * speed * Time.deltaTime);
            }

            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }


        if (isRestricting)
        {
            if (Vector3.Distance(itemToRestrict.position, transform.position) >= minDistance)
            {
                Vector3 direction = transform.position - itemToRestrict.position;

                if (direction.magnitude > 1)
                {
                    direction.Normalize();
                }
                
                Rigidbody itemBody = itemToRestrict.GetComponent<Rigidbody>();
                
                itemBody.constraints = RigidbodyConstraints.FreezePositionX;
                itemBody.constraints = RigidbodyConstraints.FreezePositionZ;
                itemBody.transform.position += direction * speed * Time.deltaTime;

            }
            else
            {
                Rigidbody itemBody = itemToRestrict.GetComponent<Rigidbody>();
                
                itemBody.constraints = RigidbodyConstraints.None;
                itemBody.constraints = RigidbodyConstraints.FreezeRotation;
            }

            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public override void OnObjectEnd()
    {
        isFollowing = false;
        isRestricting = false;

        Debug.Log("Termine cubo");

        ResetVariables(null);
        EventManager.Trigger("OnMovableUnrestrict");
        EventManager.Trigger("ResetAbility");
        lineRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        if (mySwing != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, mySwing.transform.position);
        }
        else if (isFollowing || isRestricting)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }
    }

    public void ChangeMovingMode(object[] parameters)
    {
        if (!isSwinging)
        {
            GameObject player = (GameObject) parameters[0];
            if (player.layer == LayerMask.NameToLayer("DemonPlayer"))
            {
                isFollowing = true;
                isRestricting = false;
            }
            else if (player.layer == LayerMask.NameToLayer("AngelPlayer"))
            {
                isFollowing = false;
                isRestricting = true;
            }
        }
    }

    public void DisableGravity(object[] parameters)
    {
        rb.useGravity = false;
    }

    public void CutSwingTies(object[] parameters)
    {
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        if(mySwing != null)
            rb.velocity = mySwing.lastPoint.velocity;
        
        mySwing.isHanging = false;
        mySwing.ResetStats();
        mySwing.currentItem = null;
        mySwing = null;
        transform.SetParent(null);
        
        OnItemCanceled(null);
    }

    public void OnItemCanceled(object[] parameters)
    {
        lineRenderer.enabled = false;
        CutSwingTies(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BreakableOnFall" && isFalling)
        {
            Debug.Log("ENTRE A TOMPER");
            other.gameObject.SetActive(false);
        }
    }
}
