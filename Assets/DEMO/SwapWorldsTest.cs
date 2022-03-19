using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWorldsTest : MonoBehaviour
{
    public GameObject world1;
    public GameObject world2;
    private bool trigger = true;
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (trigger == true)
            {
                world1.SetActive(true);
                world2.SetActive(false);
            }
            
            if (trigger == false)
            {
                world2.SetActive(true);
                world1.SetActive(false);
            }
            
            trigger = !trigger;
        }
    }
}
