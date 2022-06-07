using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDamagable : MonoBehaviour
{
    public bool isOneTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ITakeDamage>() != null)
        {
            other.GetComponent<ITakeDamage>().TakeDmg();
            
            if(isOneTime)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ITakeDamage>() != null)
        {
            collision.gameObject.GetComponent<ITakeDamage>().TakeDmg();
            
            if(isOneTime)
                Destroy(gameObject);
        }
    }
}
