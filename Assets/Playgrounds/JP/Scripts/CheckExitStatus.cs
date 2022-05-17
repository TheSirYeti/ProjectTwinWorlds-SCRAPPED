using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckExitStatus : MonoBehaviour
{
    public List<TotemPillar> totems;

    public Transform newPos;

    private bool flag = false;
    
    public void CheckStatus()
    {
        flag = false;
        foreach (var totem in totems)
        {
            if (!totem.isActive)
            {
                flag = true;
            }
        }

        if (!flag)
        {
            transform.position = newPos.position;
        }
    }
}
