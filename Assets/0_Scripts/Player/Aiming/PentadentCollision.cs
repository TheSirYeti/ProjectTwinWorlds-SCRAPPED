using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentadentCollision : MonoBehaviour
{
    public float duration, minDistance;
    public Transform currentDestination;

    public IEnumerator ThrowPentadent()
    {
        float time = 0;
        while (time <= duration)
        {
            yield return new WaitForSeconds(0.001f);
            time += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, currentDestination.position, time / duration);

            if (Vector3.Distance(transform.position, currentDestination.position) <= minDistance)
            {
                transform.SetParent(currentDestination);
                SoundManager.instance.PlaySound(SoundID.HIT_PENTADENT);
                StopCoroutine(ThrowPentadent());
                break;
            }
        }
    }
    
    
}
