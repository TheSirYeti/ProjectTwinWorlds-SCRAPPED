using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    public float normalSpeed, slowedSpeed, fastSpeed, currentSpeed;
    public bool shouldSpin;
    public Transform rotationPoint;

    private void Start()
    {
        currentSpeed = normalSpeed;
        StartCoroutine(Test());
    }

    private void Update()
    {
        if (shouldSpin)
        {
            transform.Rotate(0f, currentSpeed, 0f);
        }
    }

    IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            currentSpeed = slowedSpeed;
            yield return new WaitForSeconds(2f);
            currentSpeed = fastSpeed;
            yield return new WaitForSeconds(2f);
            currentSpeed = normalSpeed;
        }
    }
}
