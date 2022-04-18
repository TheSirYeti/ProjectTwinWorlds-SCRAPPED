using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyObjectCheck : MonoBehaviour
{
    public PulleySystem myPulley;

    public bool isForAngel;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object") && !myPulley.isObjectTriggered && !isForAngel)
        {
            myPulley.isObjectReady = true;
            myPulley.myObject = other.gameObject.GetComponent<Rigidbody>();
            Debug.Log("CHECK ON");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") && isForAngel)
        {
            myPulley.isAngelReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object") && !myPulley.isObjectTriggered && !isForAngel)
        {
            myPulley.isObjectReady = false;
            myPulley.myObject = null;
            Debug.Log("CHECK OUT");
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") && isForAngel)
        {
            myPulley.isAngelReady = false;
        }
    }
}
