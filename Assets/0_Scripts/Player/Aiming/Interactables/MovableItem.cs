using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class MovableItem : InteractableObject
{
    public Transform itemToFollow;
    public float minDistance, speed, velocity;
    public Rigidbody rb;
    public bool isRestricting, isFollowing, isSwinging, isFalling;
    public Transform jumpSpot;

    public Transform target;
    public SwingPhysics mySwing;
    public HangItem hangItem;
    public LineRenderer lineRenderer;

    public override void OnObjectStart()
    {
        itemToFollow = PlayerWorlds.instance.demonPlayer.transform;
        
        EventManager.UnSubscribe("OnPlayerChange", ChangeMovingMode);
        EventManager.Subscribe("OnPlayerChange", ChangeMovingMode);
        EventManager.Subscribe("ResetAbility", OnItemCanceled);

        CutSwingTies(null);
        if (mySwing != null)
        {
            transform.position = itemToFollow.transform.position;
        }

        if (hangItem != null)
        {
            hangItem.OnObjectEnd();
            hangItem = null;
        }

        lineRenderer.enabled = true;
        target = itemToFollow;
        isFollowing = true;
        isRestricting = false;
        
        isObjectTriggered = true;
    }

    public override void OnObjectDuring()
    {
        /*if (isFollowing)
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
        }*/
    }

    private void FixedUpdate()
    {
        if (isFollowing && isObjectTriggered)
        {
            if (Vector3.Distance(itemToFollow.position, transform.position) >= minDistance)
            {
                Vector3 direction = itemToFollow.position - transform.position;

                if (direction.magnitude > 1)
                {
                    direction.Normalize();
                }
                
                rb.MovePosition(transform.position += direction * speed * Time.fixedDeltaTime);
                //rb.AddForce(direction * speed, ForceMode.Impulse);

                if (!IsInSight(itemToFollow))
                {
                    StartCoroutine(CheckIfCorrectsPath());
                }
            }

            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public override void OnObjectEnd()
    {
        isFollowing = false;
        isRestricting = false;

        ResetVariables(null);
        EventManager.Trigger("OnMovableUnrestrict");
        EventManager.Trigger("ResetAbility");
        lineRenderer.enabled = false;
    }

    public override void OnObjectExecute()
    {
        if (isFollowing)
        {
            Vector3 direction = itemToFollow.position - transform.position;
            
            rb.AddForce(direction * 300f, ForceMode.Impulse);
        }
    }

    private void LateUpdate()
    {
        if (mySwing != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, mySwing.transform.position);
        }
        else if(hangItem != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hangItem.transform.position);
        }
        else if (isFollowing || isRestricting)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }
        else lineRenderer.enabled = false;
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

        if (mySwing != null)
        {
            if(mySwing.GetClosestPoint(transform) == mySwing.leftPoint)
                rb.AddForce(mySwing.transform.right * 450f, ForceMode.Impulse);
            else
                rb.AddForce(mySwing.transform.right * -450f, ForceMode.Impulse);
            mySwing.isHanging = false;
            mySwing.ResetStats();
            mySwing.currentItem = null;
            mySwing = null;
            transform.SetParent(null); 
        }

        OnItemCanceled(null);
    }

    public void OnItemCanceled(object[] parameters)
    {
        lineRenderer.enabled = false;
    }
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BreakableOnFall" && isFalling)
        {
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator CheckIfCorrectsPath()
    {
        yield return new WaitForSeconds(1f);
        if (!IsInSight(itemToFollow))
        {
            OnObjectEnd();
        }
    }
}
