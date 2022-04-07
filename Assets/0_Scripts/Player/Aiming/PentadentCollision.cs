using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentadentCollision : MonoBehaviour
{
    public float duration;
    public Vector3 currentDestination;
    public float minDistance;
    public bool foundParent;

    public GameObject currentParent;

    public IEnumerator ThrowPentadent()
    {
        float time = 0;
        while (time <= duration && !foundParent)
        {
            time += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, currentDestination, time / duration);
            
            if (Vector3.Distance(transform.position, currentDestination) <= minDistance)
            {
                SearchForParent();
                StopCoroutine(ThrowPentadent());
            }
            
            yield return new WaitForSeconds(0.001f);
        }

        SoundManager.instance.PlaySound(SoundID.HIT_PENTADENT);
        yield return new WaitForSeconds(0.0001f);
    }
    
    public void SearchForParent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.3f);
        
        List<Collider> filteredColliders = new List<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer != LayerMask.NameToLayer("Default")
                && collider.gameObject.layer != LayerMask.NameToLayer("Pentadente")
                && collider.gameObject.layer != LayerMask.NameToLayer("DemonPlayer")
                && collider.gameObject.layer != LayerMask.NameToLayer("AngelPlayer")
                && collider.gameObject.layer != LayerMask.NameToLayer("EnemyAttack"))
            {
                filteredColliders.Add(collider);
            }
        }

        if (filteredColliders.Count == 1)
        {
            Debug.Log("minho padre: " + filteredColliders[0].gameObject);
            foundParent = true;
            transform.SetParent(filteredColliders[0].transform);
            StopCoroutine(ThrowPentadent());
        }
    }                 
}
