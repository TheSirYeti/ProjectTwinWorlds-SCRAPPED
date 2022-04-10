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
    public bool isRestricting, isFollowing;
    public override void OnObjectStart()
    {
        EventManager.UnSubscribe("OnPlayerChange", ChangeMovingMode);
        EventManager.Subscribe("OnPlayerChange", ChangeMovingMode);

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
    }

    public void ChangeMovingMode(object[] parameters)
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
