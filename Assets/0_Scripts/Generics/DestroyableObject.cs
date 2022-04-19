using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public float timeToDie;
    public bool goDown;
    public bool goOut;
    public float speed;
    public Transform objectToMove;

    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    private void Update()
    {
        if (goDown)
        {
            MovementGoDown();
        }

        if (goOut)
        {
            MovementGoOut();
        }
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }

    public void MovementGoDown()
    {
        objectToMove.transform.position += Vector3.down * speed * Time.deltaTime;
    }

    public void MovementGoOut()
    {
        objectToMove.transform.position += objectToMove.transform.forward * speed * Time.deltaTime;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
