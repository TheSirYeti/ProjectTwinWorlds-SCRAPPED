using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerReference : MonoBehaviour
{
    public static LayerReference instance;

    public LayerMask obstacleMask;
    public LayerMask playerMask;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
}
