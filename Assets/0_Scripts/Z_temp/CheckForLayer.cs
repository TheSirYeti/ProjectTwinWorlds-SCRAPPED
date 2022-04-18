using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForLayer : MonoBehaviour
{
    public GameObject objectToActivate;
    public string layerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            objectToActivate.SetActive(false);
        }
    }
}
