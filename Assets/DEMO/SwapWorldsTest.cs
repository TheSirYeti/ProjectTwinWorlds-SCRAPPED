using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWorldsTest : MonoBehaviour
{
    public GameObject world1;
    public GameObject world2;
    public GameObject player1;
    public GameObject player2;
    private bool trigger = true;
    private bool battlemode = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            trigger = !trigger;

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
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            battlemode = !battlemode;
        }

        if (battlemode == true && trigger == true)
        {
            player2.transform.position = player1.transform.position;
        }
        else if (battlemode == true && trigger == false)
        {
            player1.transform.position = player2.transform.position;
        }

    }
}
