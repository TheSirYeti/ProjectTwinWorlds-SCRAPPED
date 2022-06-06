using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShaderSelectLogic : MonoBehaviour
{
    public bool isActive;

    public Material activeMat, clearMat, originalMat;
    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", SwapMaterial);
        originalMat = GetComponent<Renderer>().materials[0];

        if (isActive)
        {
            GetComponent<Renderer>().materials = new Material[] {originalMat, activeMat};
        }
        else
        {
            GetComponent<Renderer>().materials = new Material[] {originalMat, clearMat};
        }
    }

    public void SwapMaterial(object[] parameters)
    {
        if (gameObject.activeSelf)
        {
            if (GetComponent<Renderer>() != null)
            {
                if (!isActive)
                {
                    GetComponent<Renderer>().materials = new Material[] {originalMat, activeMat};
                }
                else
                {
                    GetComponent<Renderer>().materials = new Material[] {originalMat, clearMat};
                }
            }
        }

        isActive = !isActive;
    }
}
