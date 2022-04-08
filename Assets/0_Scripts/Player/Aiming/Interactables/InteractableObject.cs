using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public string layerTrigger;
    public string firstTrigger;
    public bool isFirstTriggered;
    public List<Transform> insertionPoints;
    public bool isObjectTriggered;

    private void Start()
    {
        EventManager.Subscribe("ResetAbility", ResetVariables);
        EventManager.Subscribe("ResetObject", ResetVariables);
    }

    private void Update()
    {
        if (isObjectTriggered)
        {
            OnObjectDuring();
        }
    }

    public abstract void OnObjectStart();

    public abstract void OnObjectDuring();

    public abstract void OnObjectEnd();

    public bool CheckForFirstTrigger()
    {
        return isFirstTriggered;
    }

    public void ResetVariables(object[] parameters)
    {
        isFirstTriggered = false;
        isObjectTriggered = false;
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

}
