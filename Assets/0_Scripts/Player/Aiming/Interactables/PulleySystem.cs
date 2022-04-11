using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public override void OnObjectEnd()
    {
        ResetVariables(null);
        EventManager.Trigger("ResetAbility");
        EventManager.Trigger("OnPulleyStop");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Movable Object") && !isObjectTriggered)
        {
            isObjectReady = true;
            myObject = collision.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object") && !isObjectTriggered)
        {
            isObjectReady = false;
            myObject = null;
        }
    }
}
