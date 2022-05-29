using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FRadius : MonoBehaviour
{
    [SerializeField] private float waitTime = 5f;
    private bool isStillInTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger") ||
            other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            isStillInTrigger = true;
            StartCoroutine(FRadiusBuffer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger") ||
            other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            isStillInTrigger = false;
            EventManager.Trigger("OnHideF");
        }
    }

    IEnumerator FRadiusBuffer()
    {
        yield return new WaitForSeconds(waitTime);
        if(isStillInTrigger)
            EventManager.Trigger("OnShowF");
    }
}
