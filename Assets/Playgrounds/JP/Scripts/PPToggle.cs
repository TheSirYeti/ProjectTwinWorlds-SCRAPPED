using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPToggle : MonoBehaviour
{
    public GameObject demonPP, angelPP;
    public bool isDemon;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isDemon = !isDemon;
            if (isDemon)
            {
                angelPP.SetActive(false);
                demonPP.SetActive(true);
            }
            else {demonPP.SetActive(false); angelPP.SetActive(true);}
        }
    }
}
