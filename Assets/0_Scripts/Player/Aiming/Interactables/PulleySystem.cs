using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PulleySystem : InteractableObject
{
    public Transform playerStartPosition, objectStartPosition, demon;
    public Rigidbody myObject;
    public bool isObjectReady;

    public LineRenderer lineRenderer;

    public override void OnObjectStart()
    {
        if (isObjectReady)
        {
            lineRenderer.enabled = true;
            isObjectTriggered = true;

            myObject.transform.position = objectStartPosition.position;
            demon.transform.position = playerStartPosition.position;
            EventManager.Trigger("OnPulleyStart", myObject);
            Debug.Log("Start");
        }
        else
        {
            Debug.Log("Fail");
            OnObjectEnd();
        }
    }

    public override void OnObjectDuring()
    {
        if (isObjectTriggered)
        {
            lineRenderer.SetPosition(0, demon.position);
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.SetPosition(2, myObject.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public override void OnObjectEnd()
    {
        lineRenderer.enabled = false;
        ResetVariables(null);
        EventManager.Trigger("ResetAbility");
        EventManager.Trigger("OnPulleyStop");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object") && isObjectTriggered)
        {
            myObject.transform.position = playerStartPosition.position;
            OnObjectEnd();
        }
    }
}
