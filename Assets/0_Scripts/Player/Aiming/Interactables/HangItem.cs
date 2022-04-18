using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangItem : InteractableObject
{
    public Transform holdPoint;

    public float minDistance, velocityForce;
    
    public List<MovableItem> movableItems;
    
    public Transform angelAttack, demonAttack;
    public MovableItem currentItem;
    
    public bool isOnLeft;
    public bool isHanging;
    public bool isAfterHang;

    public LineRenderer lineRenderer;
    
    public override void OnObjectStart()
    {
        if (angelAttack.GetComponent<AngelAttacks>().canHang)
        {
            isHanging = true;
            currentItem = movableItems[FindNearestItem(angelAttack)];
            currentItem.rb.useGravity = false;
            currentItem.transform.position = holdPoint.transform.position;
            currentItem.transform.SetParent(holdPoint);
            currentItem.hangItem = this;
            isObjectTriggered = true;
        }

        OnObjectEnd();
        
    }

    public override void OnObjectDuring()
    {
        if (currentItem != null)
        {
            lineRenderer.SetPosition(0, currentItem.transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
        else
        {
            isHanging = false;
            OnObjectEnd();
        }
    }

    public override void OnObjectEnd()
    {
        if (!isHanging && currentItem != null)
        {
            currentItem.rb.useGravity = true;
            currentItem.lineRenderer.enabled = false;
            currentItem.transform.SetParent(null);
            currentItem = null;
            lineRenderer.enabled = false;
        }
        else
        {
            ResetVariables(null);
            EventManager.Trigger("ResetAbility");
        }
    }

    public override void OnObjectExecute()
    {
        throw new System.NotImplementedException();
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
}
