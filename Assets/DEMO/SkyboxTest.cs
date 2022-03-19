using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxTest : MonoBehaviour
{
    public Material mat1;
    public Material mat2;

    private bool trigger = true;
    
    private void Start()
    {
        RenderSettings.skybox = mat1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (trigger == true)
            {
                RenderSettings.skybox = mat1;
            }
            
            if (trigger == false)
            {
                RenderSettings.skybox = mat2;
            }
            
            trigger = !trigger;
        }
    }
}
