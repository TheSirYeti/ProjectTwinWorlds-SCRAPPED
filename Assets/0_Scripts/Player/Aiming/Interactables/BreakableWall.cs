using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

internal class BreakableWall : InteractableObject
{
    public LineRenderer lineRenderer;
    public List<MeshDestroy> destroyableMeshes;
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
        SoundManager.instance.PlaySound(SoundID.WOOD_BREAK);
        StartCoroutine(BreakSubObjects());
        Destroy(gameObject);
    }

    public override void OnObjectExecute()
    {
        OnObjectEnd();
    }

    public IEnumerator BreakSubObjects()
    {
        foreach (MeshDestroy mesh in destroyableMeshes)
        {
            mesh.DestroyMesh();
        }

        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
