using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public float timeToDie;
    public bool goDown;
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
            objectToMove.transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }
}
