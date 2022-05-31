using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    public KeyCode keyToPress;
    public List<GameObject> objectsToEnable;
    public float minDistance;
    private GameObject myPlayer;
    
    public bool shouldEnable;
    
    private void Update()
    {
        if (Input.GetKeyDown(keyToPress) && HasNearbyPlayer())
        {
            ToggleObjects();
        }
    }

    bool HasNearbyPlayer()
    {
        if (Vector3.Distance(transform.position, myPlayer.transform.position) <=
            minDistance)
        {
            return true;
        }

        return false;
    }

    void ToggleObjects()
    {
        foreach (var obj in objectsToEnable)
        {
            obj.SetActive(shouldEnable);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            myPlayer = other.gameObject;
        }
    }
}
