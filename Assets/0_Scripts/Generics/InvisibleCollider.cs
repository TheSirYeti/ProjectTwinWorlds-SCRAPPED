using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibleCollider : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
