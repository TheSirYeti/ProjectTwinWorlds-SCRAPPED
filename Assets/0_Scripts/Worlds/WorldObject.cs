using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public Material angelOffMaterial, demonOffMaterial, clearMaterial;
    public Material defaultMaterial;
    public Collider collider;

    public bool isDemon;
    public bool isWorldActive;
    public bool invisible;
    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", SwapMaterial);
        defaultMaterial = GetComponent<Renderer>().material;
        collider = GetComponent<Collider>();

        if (!isWorldActive)
        {
            isWorldActive = false;
            
            if (!invisible)
            {
                if (isDemon)
                {
                    GetComponent<Renderer>().material = demonOffMaterial;
                } 
                else GetComponent<Renderer>().material = angelOffMaterial;
            }
            else
            {
                GetComponent<Renderer>().material = clearMaterial;
            }

            if (collider != null) 
                collider.enabled = false;
        }
        else
        {
            isWorldActive = true;
            GetComponent<Renderer>().material = defaultMaterial;
            if (collider != null) 
                collider.enabled = true;
        }
    }

    public void SwapMaterial(object[] parameters)
    {
        GameObject myPlayer = (GameObject) parameters[0];

        if (GetComponent<Renderer>() != null)
        {
            if (isWorldActive)
            {
                if (!invisible)
                {
                    if (myPlayer.layer == LayerMask.NameToLayer("DemonPlayer"))
                        GetComponent<Renderer>().material = angelOffMaterial;
                    else GetComponent<Renderer>().material = demonOffMaterial;
                }
                else
                {
                    GetComponent<Renderer>().material = clearMaterial;
                }

                isWorldActive = false;
                if (collider != null) 
                    collider.enabled = false;
            }
            else
            {
                isWorldActive = true;
                GetComponent<Renderer>().material = defaultMaterial;
                if (collider != null) 
                    collider.enabled = true;
            }
        }
    }
}
