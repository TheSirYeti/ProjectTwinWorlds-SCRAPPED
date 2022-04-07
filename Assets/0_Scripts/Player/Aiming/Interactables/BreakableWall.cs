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
        ResetVariables(null);
        gameObject.SetActive(false);
    }
}
