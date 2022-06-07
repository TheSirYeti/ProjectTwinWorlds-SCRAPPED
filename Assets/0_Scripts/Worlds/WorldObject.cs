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

        if (GetComponent<Renderer>() != null)
            defaultMaterial = GetComponent<Renderer>().material;

        if (GetComponent<Collider>() != null)
            collider = GetComponent<Collider>();

        if (!isWorldActive)
        {
            isWorldActive = false;

            if (!invisible)
            {
                GetComponent<Renderer>().enabled = true;
                if (isDemon)
                {
                    GetComponent<Renderer>().material = demonOffMaterial;
                }
                else GetComponent<Renderer>().material = angelOffMaterial;
            }
            else
            {
                if (GetComponent<Renderer>() != null)
                    GetComponent<Renderer>().enabled = false;
            }

            if (collider != null)
                collider.enabled = false;
        }
        else
        {
            isWorldActive = true;
            GetComponent<Renderer>().enabled = true;
            GetComponent<Renderer>().material = defaultMaterial;
            if (collider != null)
                collider.enabled = true;
        }
    }

    public void SwapMaterial(object[] parameters)
    {
        if (GetComponent<Renderer>() != null)
        {
            if (isWorldActive)
            {
                if (!invisible)
                {
                    GetComponent<Renderer>().enabled = true;
                    if (isDemon)
                        GetComponent<Renderer>().material = angelOffMaterial;
                    else GetComponent<Renderer>().material = demonOffMaterial;
                }
                else
                {
                    GetComponent<Renderer>().enabled = false;
                }

                isWorldActive = false;
                if (collider != null)
                    collider.enabled = false;
            }
            else
            {
                isWorldActive = true;
                GetComponent<Renderer>().enabled = true;
                GetComponent<Renderer>().material = defaultMaterial;
                if (collider != null)
                    collider.enabled = true;
            }
        }
    }
}
