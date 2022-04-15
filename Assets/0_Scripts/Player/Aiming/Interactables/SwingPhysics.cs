using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class SwingPhysics : InteractableObject
{
    public Rigidbody lastPoint;
    public Transform holdPoint;

    public float minDistance, velocityForce;
    
    public List<MovableItem> movableItems;
    public List<Rigidbody> hingePoints;
    
    public Transform angelAttack, demonAttack;
    public MovableItem currentItem;
    
    public bool isOnLeft;
    public bool isHanging;
    public bool isAfterHang;
    
    public override void OnObjectStart()
    {
        if (isOnLeft)
        {
            foreach (Rigidbody rb in hingePoints)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
            }
        }
        else
        {
            foreach (Rigidbody rb in hingePoints)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX;
            }
        }
        
        if (angelAttack.GetComponent<AngelAttacks>().canHang)
        {
            isHanging = true;
            currentItem = movableItems[FindNearestItem(angelAttack)];
            currentItem.rb.useGravity = false;
            currentItem.mySwing = this;
            currentItem.transform.SetParent(holdPoint);
            currentItem.lineRenderer.enabled = true;
            currentItem.transform.position = holdPoint.transform.position;
            currentItem.transform.SetParent(holdPoint);


            Debug.Log("Cuelgo caja");
            OnObjectEnd();
        }
        else
        {
            Debug.Log("Me cuelgo yo");
            EventManager.Trigger("OnSwingStart", lastPoint, holdPoint, isOnLeft);
            isObjectTriggered = true;
        }
        
        lastPoint.AddForce(lastPoint.transform.right * velocityForce, ForceMode.Acceleration);
    }

    public override void OnObjectDuring()
    {
        if (currentItem != null)
        {
            if (currentItem.isObjectTriggered || !isHanging)
            {
                OnObjectEnd();
            }
        }
    }

    public override void OnObjectEnd()
    {
        if (!isHanging)
        {
            if (!currentItem.isObjectTriggered && currentItem != null)
            {
                currentItem.rb.useGravity = true;
                currentItem.transform.SetParent(null);
                currentItem = null;
            }
            else
            {
                ResetVariables(null);
                EventManager.Trigger("OnSwingStop");
                isObjectTriggered = false;
            }
        }
        else
        {
            ResetVariables(null);
            isAfterHang = true;
        }
        
        EventManager.Trigger("ResetAbility");

        foreach (Rigidbody rb in hingePoints)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        if (isHanging && isFirstTriggered && isAfterHang)
        {
            currentItem.CutSwingTies(null);
        }
        else if (currentItem != null)
        {
            currentItem.transform.position = holdPoint.transform.position;
        }


    }

    public int FindNearestItem(Transform myTransform)
    {
        float minDistance = Mathf.Infinity;
        int currentID = -1;

        for(int i = 0; i < movableItems.Count; i++)
        {
            float distance = Vector3.Distance(myTransform.position, movableItems[i].transform.position);

            if (distance <= minDistance)
            {
                minDistance = distance;
                currentID = i;
            }
        }
        
        return currentID;
    }

    public IEnumerator GoToSwing(float duration)
    {
        float currentTime = 0f;
        
        
        while (Vector3.Distance(holdPoint.transform.position, currentItem.transform.position) >= minDistance)
        {
            currentTime += Time.deltaTime;
            Vector3 direction = holdPoint.transform.position - currentItem.transform.position;

            currentItem.transform.position =
                Vector3.Lerp(holdPoint.position, direction.normalized, currentTime / duration);

            yield return new WaitForSeconds(0.001f);
        }

        currentItem.transform.position = holdPoint.position;
        currentItem.transform.SetParent(holdPoint);
        yield return new WaitForSeconds(0.001f);
    }

    public void ResetStats()
    {
        ResetVariables(null);
        isHanging = false;
        isAfterHang = false;
    }
}
