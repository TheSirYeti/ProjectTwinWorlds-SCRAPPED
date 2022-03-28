using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentadentCollision : MonoBehaviour
{
    public Collider collider;
    public float duration;
    public Vector3 currentDestination;
    public float minDistance;
    public IEnumerator ThrowPentadent()
    {
        float time = 0;
        while (time <= duration)
        {
            time += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, currentDestination, time / duration);
            
            if (Vector3.Distance(transform.position, currentDestination) <= minDistance)
            {
                SearchForParent();
                StopCoroutine(ThrowPentadent());
            }
            
            yield return new WaitForSeconds(0.005f);
        }
    }
    
    public void SearchForParent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        
        List<Collider> filteredColliders = new List<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer != LayerMask.NameToLayer("Default")
                && collider.gameObject.layer != LayerMask.NameToLayer("Wall")
                && collider.gameObject.layer != LayerMask.NameToLayer("Floor")
                && collider.gameObject.layer != LayerMask.NameToLayer("Pentadente"))
            {
                filteredColliders.Add(collider);
            }
        }

        if (filteredColliders.Count == 1)
        {
            Debug.Log("minho padre: " + filteredColliders[0].gameObject);
            transform.SetParent(filteredColliders[0].transform);
        }
    }
}
