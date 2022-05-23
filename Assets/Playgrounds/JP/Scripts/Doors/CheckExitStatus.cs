using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckExitStatus : MonoBehaviour
{
    public List<GameObject> objectsWithInterface;
    private List<IDoorTriggerable> doorTriggerables = new List<IDoorTriggerable>();

    public Transform newPos, oldPos;

    private bool flag = false;

    private void Start()
    {
        foreach (var obj in objectsWithInterface)
        {
            doorTriggerables.Add(obj.GetComponent<IDoorTriggerable>());
        }
    }


    public void CheckStatus()
    {
        flag = false;
        foreach (var button in doorTriggerables)
        {
            if (!button.IsTriggerableActive())
            {
                flag = true;
            }
        }

        if (!flag)
        {
            transform.position = newPos.position;
        }
        else
        {
            transform.position = oldPos.position;
        }
    }
}
