using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericLever : MonoBehaviour
{
    public Animator myLever;
    public bool canInteract;
    public float minDistance;
    public GameObject currentHolder;
    
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canInteract = true;
            currentHolder = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canInteract = false;
            currentHolder = null;
        }
    }

    bool HasNearbyPlayer()
    {
        if (currentHolder != null)
        {
            if (Vector3.Distance(transform.position, currentHolder.transform.position) <=
                minDistance)
            {
                return true;
            }
        }

        return false;
    }

    public virtual void OnLeverFlicked() {}
}

