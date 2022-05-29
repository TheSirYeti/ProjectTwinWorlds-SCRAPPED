using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericLever : MonoBehaviour
{
    public Animator myLever;
    public bool canInteract;
    public float minDistance;

    private bool hasInteracted = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canInteract && !hasInteracted && HasNearbyPlayer())
        {
            hasInteracted = true;
            myLever.Play("SwitchActivate");
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
            canInteract = false;
        }
    }

    bool HasNearbyPlayer()
    {
        if (PlayerWorlds.instance.demonPlayer.activeSelf)
        {
            if (Vector3.Distance(transform.position, PlayerWorlds.instance.demonPlayer.transform.position) <=
                minDistance)
            {
                return true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerWorlds.instance.angelPlayer.transform.position) <=
                minDistance)
            {
                return true;
            }
        }

        return false;
    }

    public virtual void OnLeverFlicked() {}
}

