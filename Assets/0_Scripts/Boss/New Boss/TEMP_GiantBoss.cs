using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_GiantBoss : MonoBehaviour
{
    public Transform pos1, pos2;
    public float size1, size2;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            transform.position = pos2.position;
            transform.rotation = pos2.rotation;

            GetComponent<Camera>().orthographicSize = size2;
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            transform.position = pos1.position;
            transform.rotation = pos1.rotation;
            GetComponent<Camera>().orthographicSize = size1;
        }
    }
}
