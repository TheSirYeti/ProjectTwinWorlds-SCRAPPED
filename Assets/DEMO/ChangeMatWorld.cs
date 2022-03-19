using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMatWorld : MonoBehaviour
{
    public Material mat1;
    public Material mat2;

    private Renderer _renderer;
    private bool trigger = true;
    
    private void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (trigger == true)
            {
                _renderer.material = mat1;
            }
            
            if (trigger == false)
            {
                _renderer.material = mat2;
            }
            
            trigger = !trigger;
        }
    }
}
