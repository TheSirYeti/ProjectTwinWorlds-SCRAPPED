using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BreakableWall : InteractableObject
{
    public LineRenderer lineRenderer;
    public override void OnObjectStart()
    {
        EventManager.Subscribe("ResetAbility", ResetVariables);
        EventManager.Subscribe("ResetObject", ResetVariables);
        EventManager.Subscribe("OnAbilityCancel", CancelAbility);
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        isObjectTriggered = true;
    }

    public override void OnObjectDuring()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, PlayerWorlds.instance.demonPlayer.transform.position);
    }

    public override void OnObjectEnd()
    {
        lineRenderer.enabled = false;
        ResetVariables(null);
        EventManager.Trigger("ResetAbility");
        gameObject.SetActive(false);
    }

    public override void OnObjectExecute()
    {
        OnObjectEnd();
    }
}
