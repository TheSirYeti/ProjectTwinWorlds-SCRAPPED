using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Interactable
{
    public Material matOn, matOff;
    public MeshRenderer renderer;

    public DoorTrigger doorTrigger;
    
    public override void EnableInteractable()
    {
        isEnabled = true;
        renderer.material = matOn;

        doorTrigger.CheckInteractableStatus();
    }

    public override void DisableInteractable()
    {
        isEnabled = false;
        renderer.material = matOff;

        doorTrigger.CheckInteractableStatus();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer")
            || other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer")
            || other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            EnableInteractable();
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer")
            || other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer")
            || other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            EnableInteractable();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer")
            || other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer")
            || other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            DisableInteractable();
        }
    }
}
