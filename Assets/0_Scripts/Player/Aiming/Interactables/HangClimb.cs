using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HangClimb : InteractableObject
{
    public Transform startPoint, heightPoint, angel;
    public bool isOnLeft;
    
    
    public override void OnObjectStart()
    {
        angel.position = startPoint.position;
        EventManager.Trigger("OnClimbStart", isOnLeft);
        isObjectTriggered = true;
    }

    public override void OnObjectDuring()
    {
        if (angel.position.y >= heightPoint.position.y)
        {
            angel.position = new Vector3(angel.position.x, heightPoint.position.y, angel.position.z);
        }
    }

    public override void OnObjectEnd()
    {
        EventManager.Trigger("OnClimbStop");
        EventManager.Trigger("ResetAbility");
        ResetVariables(null);
    }
}
