using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : InteractableObject
{
    public override void OnObjectStart()
    {
        isObjectTriggered = true;
    }

    public override void OnObjectDuring()
    {
        //nothing
    }

    public override void OnObjectEnd()
    {
        Debug.Log("Porque?");
        ResetVariables(null);
        EventManager.Trigger("ResetAbility");
        gameObject.SetActive(false);
    }
}
