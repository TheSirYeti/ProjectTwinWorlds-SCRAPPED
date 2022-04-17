using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public Material angelOffMaterial, demonOffMaterial;
    public Material defaultMaterial;
    public Collider collider;

    public bool isDemon;
    public bool isWorldActive;
    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", SwapMaterial);
        defaultMaterial = GetComponent<Renderer>().material;

        if (isWorldActive)
        {
            isWorldActive = false;
            
            if (isDemon)
            {
                GetComponent<Renderer>().material = demonOffMaterial;
            } 
            else GetComponent<Renderer>().material = angelOffMaterial;
            
            collider.enabled = false;
        }
        else
        {
            isWorldActive = true;
            GetComponent<Renderer>().material = defaultMaterial;
            collider.enabled = true;
        }
    }

    public void SwapMaterial(object[] parameters)
    {
        GameObject myPlayer = (GameObject) parameters[0];
        
        if (isWorldActive)
        {
            if (myPlayer.layer == LayerMask.NameToLayer("DemonPlayer"))
                GetComponent<Renderer>().material = demonOffMaterial;
            else GetComponent<Renderer>().material = angelOffMaterial;

            isWorldActive = false;
            collider.enabled = false;
        }
        else
        {
            isWorldActive = true;
            GetComponent<Renderer>().material = defaultMaterial;
            collider.enabled = true;
        }
    }
}
