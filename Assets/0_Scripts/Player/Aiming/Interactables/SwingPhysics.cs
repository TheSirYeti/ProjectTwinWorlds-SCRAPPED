using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingPhysics : InteractableObject
{
    public Rigidbody lastPoint;
    public Transform holdPoint;
    
    public override void OnObjectStart()
    {
        EventManager.Trigger("OnSwingStart", lastPoint, holdPoint);
        isObjectTriggered = true;
    }

    public override void OnObjectDuring()
    {
        
    }

    public override void OnObjectEnd()
    {
        EventManager.Trigger("OnSwingStop");
        isObjectTriggered = false;
    }
}
