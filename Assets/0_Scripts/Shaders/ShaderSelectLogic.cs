using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShaderSelectLogic : MonoBehaviour
{
    public bool amDemon;
    public bool isActive;

    public Material demonMat, angelMat, clearMat;
    public Renderer myRenderer;

    public int matPositionID;
    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        EventManager.Subscribe("OnPlayerChange", SwapMaterial);
    }

    void SwapMaterial(object[] parameters)
    {
        if (isActive)
        {
            if (amDemon)
            {
                myRenderer.materials[matPositionID] = demonMat;
            }
            else
            {
                myRenderer.materials[matPositionID] = angelMat;
            }
        }
        else
        {
            myRenderer.materials[matPositionID] = clearMat;
        }
    }
}
