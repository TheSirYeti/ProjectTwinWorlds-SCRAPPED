using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TotemButton : MonoBehaviour
{
    public TotemPillar pillar;
    public float minDistance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsInDistance())
        {
            pillar.EnableTotem();
        }
    }

    bool IsInDistance()
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
}
