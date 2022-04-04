using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    public float currentSpeed;
    public List<float> speedValues;
    public bool shouldSpin;
    public List<GameObject> shields;

    private void Update()
    {
        if (shouldSpin)
        {
            transform.Rotate(0f, currentSpeed, 0f);
        }
    }

    public void SetSpeed(SpeedState state)
    {
        currentSpeed = speedValues[(int) state];
    }
    
    public enum SpeedState
    {
        SLOW,
        NORMAL,
        FAST
    }

    public void ResetShields()
    {
        foreach (var shield in shields)
        {
            shield.SetActive(true);
        }
    }
}
