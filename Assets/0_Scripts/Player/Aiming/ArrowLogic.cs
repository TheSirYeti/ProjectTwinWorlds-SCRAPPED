using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    public float timeToDespawn;
    public float speed;
    
    private void Update()
    {
        if (timeToDespawn >= 0)
        {
            timeToDespawn -= Time.fixedDeltaTime;
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
