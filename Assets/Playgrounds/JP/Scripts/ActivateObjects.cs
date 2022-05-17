using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    public KeyCode keyToPress;
    public List<GameObject> objectsToEnable;
    public float minDistance;
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
        Debug.Log(Vector3.Distance(transform.position, PlayerWorlds.instance.demonPlayer.transform.position));
        Debug.Log(Vector3.Distance(transform.position, PlayerWorlds.instance.angelPlayer.transform.position));
        
        if (PlayerWorlds.instance.demonPlayer.activeSelf)
        {
            if (Vector3.Distance(transform.position, PlayerWorlds.instance.demonPlayer.transform.position) <=
                minDistance)
            {
                return true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerWorlds.instance.angelPlayer.transform.position) <=
                minDistance)
            {
                return true;
            }
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
}
