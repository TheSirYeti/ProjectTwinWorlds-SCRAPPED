using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUPERTEMPSCRIPTJSADJASDJ : MonoBehaviour
{
    private bool isSpinning;
    private bool canPress;
    public GameObject objectToDisable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canPress)
        {
            StartCoroutine(DoSpin());
            objectToDisable.SetActive(false);
        }

        if (isSpinning)
        {
            transform.Rotate(new Vector3(0f, 5f, 0f));
        }
    }

    IEnumerator DoSpin()
    {
        isSpinning = true;
        yield return new WaitForSeconds(2f);
        isSpinning = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger") || other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            canPress = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger") || other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            canPress = false;
        }
    }
}
