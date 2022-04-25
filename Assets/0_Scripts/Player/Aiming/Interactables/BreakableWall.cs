using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BreakableWall : InteractableObject
{
    public override void OnObjectStart()
    {
        isObjectTriggered = true;
    }

    public override void OnObjectDuring()
    {
        //
    }

    public override void OnObjectEnd()
    {
        ResetVariables(null);
        EventManager.Trigger("ResetAbility");
        gameObject.SetActive(false);
    }

    public override void OnObjectExecute()
    {
        OnObjectEnd();
    }
}
