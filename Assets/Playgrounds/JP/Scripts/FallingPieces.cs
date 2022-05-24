using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPieces : MonoBehaviour
{
    public List<Transform> myPlatforms;
    
    List<Vector3> topPositions, bottomPositions;
    bool isMoving = true;

    private void Start()
    {
        topPositions = new List<Vector3>();
        bottomPositions = new List<Vector3>();
        
        foreach (var platform in myPlatforms)
        {
            topPositions.Add(platform.position);
            bottomPositions.Add(platform.position + (Vector3.down * 2));
            platform.position += (Vector3.down * 2);
        }

        StartCoroutine(SmallBuffer());
    }

    private IEnumerator StartPlatformMovement()
    {
        while (isMoving)
        {
            for (int i = 0; i < myPlatforms.Count; i++)
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(MovePlatform(i));
            }
        }
    }

    private IEnumerator MovePlatform(int index)
    {
        myPlatforms[index].position = topPositions[index];
        yield return new WaitForSeconds(3f);
        myPlatforms[index].position = bottomPositions[index];
    }

    IEnumerator SmallBuffer()
    {
        StartCoroutine(StartPlatformMovement());
        yield return new WaitForSeconds(6f);
        StartCoroutine(StartPlatformMovement());
        yield return new WaitForSeconds(6f);
        StartCoroutine(StartPlatformMovement());
        yield return new WaitForSeconds(6f);
        StartCoroutine(StartPlatformMovement());
        
    }

    void StopMovement(object[] parameters)
    {
        isMoving = false;
        StopCoroutine(StartPlatformMovement());
    }
}
