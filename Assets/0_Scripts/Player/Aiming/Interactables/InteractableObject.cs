using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public int id;
    public string layerTrigger;
    public string firstTrigger;
    public bool isFirstTriggered;
    public List<Transform> insertionPoints;
    public bool isObjectTriggered;
    public float triggerDistance;
    public GameObject myShader;
    
    private void Start()
    {
        EventManager.Subscribe("ResetAbility", ResetVariables);
        EventManager.Subscribe("ResetObject", ResetVariables);
        EventManager.Subscribe("OnAbilityCancel", CancelAbility);
    }

    private void Update()
    {
        if (isObjectTriggered)
        {
            OnObjectDuring();
            myShader.SetActive(false);
        }
        else 
        if(isFirstTriggered 
                && Vector3.Distance(PlayerWorlds.instance.currentPlayer.transform.position, PlayerWorlds.instance.firstTriggerPlayer.transform.position) <= triggerDistance
                && PlayerWorlds.instance.currentPlayer != PlayerWorlds.instance.firstTriggerPlayer
                && IsInSight(PlayerWorlds.instance.currentPlayer.transform))
        {
            myShader.SetActive(true);
        } else myShader.SetActive(false);
    }

    public abstract void OnObjectStart();

    public abstract void OnObjectDuring();

    public abstract void OnObjectEnd();

    public abstract void OnObjectExecute();

    public bool CheckForFirstTrigger()
    {
        return isFirstTriggered;
    }

    public void ResetVariables(object[] parameters)
    {
        isFirstTriggered = false;
        isObjectTriggered = false;
    }

    public void CancelAbility(object[] parameters)
    {
        if (isObjectTriggered)
        {
            OnObjectEnd();
        }
    }
    
    public int GetClosestInsertionPoint(Vector3 position)
    {
        int closest = -1;
        float minDistance = Mathf.Infinity;
        
        for(int i = 0; i < insertionPoints.Count; i++)
        {
            float distance = Vector3.Distance(insertionPoints[i].position, position);

            if (distance <= minDistance)
            {
                closest = i;
                minDistance = distance;
            }
        }

        return closest;
    }

    public bool CanInteract(Transform myTransform, bool isDemon)
    {
        Transform referencePoint;

        if (isFirstTriggered)
        {
            if (!isDemon)
                referencePoint = PlayerWorlds.instance.demonTotem.transform;
            else referencePoint = PlayerWorlds.instance.angelTotem.transform;
        }
        else return true;

        if (Vector3.Distance(referencePoint.position, myTransform.position) <= triggerDistance)
        {
            return true;
        }
        else return false;
    }

    public bool IsInSight(Transform myTransform)
    {
        Vector3 dirToTarget = (myTransform.position - transform.position);
        if (!Physics.Raycast(transform.position, dirToTarget, dirToTarget.magnitude,
                LayerReference.instance.obstacleMask))
        {
            return true;
        }
        
        return false;
    }

}
