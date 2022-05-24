using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FRadius : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger") ||
            other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            EventManager.Trigger("OnShowF");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger") ||
            other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            EventManager.Trigger("OnHideF");
        }
    }
}
