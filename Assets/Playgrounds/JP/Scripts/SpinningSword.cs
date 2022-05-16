using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSword : MonoBehaviour
{
    public Vector3 rotationValues;
    private void Update()
    {
        transform.Rotate(rotationValues * Time.deltaTime);
    }
}
