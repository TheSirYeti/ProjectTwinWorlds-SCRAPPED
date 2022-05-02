using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class SwingPhysics : InteractableObject
{
    public Transform holdPoint;

    public float minDistance, velocityForce;
    public Animator animator;
    
    
    public List<MovableItem> movableItems;

    public Transform leftPoint, rightPoint;
    public Transform angelAttack;
    public MovableItem currentItem;
    
    public bool isOnLeft;
    public bool isHanging;
    public bool isAfterHang;
    
    public override void OnObjectStart()
    {
        angelAttack = PlayerWorlds.instance.angelPlayer.transform;
        Debug.Log(angelAttack);
        
        if (GetClosestPoint(angelAttack) == leftPoint)
        {
            animator.SetBool("swingLeft", true);
        }
        else
        {
            animator.SetBool("swingRight", true);
        }

        if (angelAttack.GetComponent<AngelAttacks>().canHang)
        {
            isHanging = true;
            currentItem = movableItems[FindNearestItem(angelAttack)];
            currentItem.rb.useGravity = false;
            currentItem.mySwing = this;
            currentItem.lineRenderer.enabled = true;
            currentItem.transform.position = holdPoint.transform.position;
            currentItem.transform.SetParent(holdPoint);
        }
        else
        {
            Debug.Log("Me cuelgo yo");
            EventManager.Trigger("OnSwingStart",this, holdPoint, isOnLeft);
        }
        
        isObjectTriggered = true;
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
            if (currentItem != null && !currentItem.isObjectTriggered)
            {
                currentItem.CutSwingTies(null);
                ResetVariables(null);
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
        animator.SetBool("swingLeft", false);
        animator.SetBool("swingRight", false);
        EventManager.Trigger("ResetAbility");
    }
    
    public override void OnObjectExecute()
    {
        
    }

    private void LateUpdate()
    {
        if (isHanging && !isFirstTriggered && isAfterHang)
        {
            if (currentItem != null)
            {
                currentItem.CutSwingTies(null);
                isHanging = false;
                OnObjectEnd();
                Debug.Log("ME FUI RAJE AL CUBO");
            }
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

    public Transform GetClosestPoint(Transform reference)
    {
        float leftDistance = Vector3.Distance(reference.position, leftPoint.position);
        float rightDistance = Vector3.Distance(reference.position, rightPoint.position);

        if (leftDistance <= rightDistance)
        {
            return leftPoint;
        }
        return rightPoint;
    }

    public void ResetStats()
    {
        ResetVariables(null);
        isHanging = false;
        isAfterHang = false;
    }
}
