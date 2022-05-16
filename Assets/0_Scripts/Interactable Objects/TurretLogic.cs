using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float initialDelay, totalDelay;

    private void Start()
    {
        StartCoroutine(DoShooting());
    }

    IEnumerator DoShooting()
    {
        yield return new WaitForSeconds(initialDelay);
        
        while (true)
        {
            var bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.position = transform.position;
            bulletInstance.transform.Rotate(new Vector3(0, -90f, 0));
            yield return new WaitForSeconds(totalDelay);
        }
    }
}
