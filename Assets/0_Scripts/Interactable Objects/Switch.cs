using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    public Material matOn, matOff;
    public Transform posOn, posOff;
    public MeshRenderer renderer;
    public Transform lever;

    public bool isInRange;
    public DoorTrigger doorTrigger;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            if (!isEnabled)
            {
                EnableInteractable();
            } else DisableInteractable();
        }
    }

    public override void EnableInteractable()
    {
        isEnabled = true;
        renderer.material = matOn;
        lever.position = posOn.position;
        lever.rotation = posOn.rotation;
        
        doorTrigger.CheckInteractableStatus();
    }

    public override void DisableInteractable()
    {
        isEnabled = false;
        renderer.material = matOff;
        lever.position = posOff.position;
        lever.rotation = posOff.rotation;
        
        doorTrigger.CheckInteractableStatus();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") ||
            other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            isInRange = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") ||
            other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            isInRange = false;
        }
    }
}
