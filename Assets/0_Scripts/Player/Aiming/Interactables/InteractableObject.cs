using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public string layerTrigger;
    public string firstTrigger;
    public bool isFirstTriggered;
    public Transform insertionPoint;
    public bool isObjectTriggered;

    private void Start()
    {
        EventManager.Subscribe("ResetAbility", ResetVariables);
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

    public bool CheckForTrigger()
    {
        return isFirstTriggered;
    }

    public void ResetVariables(object[] parameters)
    {
        isFirstTriggered = false;
        isObjectTriggered = false;
    }
}
