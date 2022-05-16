using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridgeLever : MonoBehaviour
{
    public Animator myBridge;
    public bool canInteract;
    public GameObject flickOn, flickOff;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canInteract && !flickOff.activeSelf)
        {
            myBridge.Play("BridgeFall");
            flickOff.SetActive(true);
            flickOn.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") || other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") || other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            canInteract = true;
        }
    }
}
