using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyObjectCheck : MonoBehaviour
{
    public PulleySystem myPulley;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object") && !myPulley.isObjectTriggered)
        {
            myPulley.isObjectReady = true;
            myPulley.myObject = other.gameObject.GetComponent<Rigidbody>();
            Debug.Log("CHECK ON");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object") && !myPulley.isObjectTriggered)
        {
            myPulley.isObjectReady = false;
            myPulley.myObject = null;
            Debug.Log("CHECK OUT");
        }
    }
}
