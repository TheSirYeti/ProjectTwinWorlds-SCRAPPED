using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TotemButton : MonoBehaviour
{
    public KeyCode cheatKey;
    public TotemPillar pillar;
    public float minDistance;
    private bool isTriggered = false;
    
    public List<GameObject> objectsToChangeMat;
    public GameObject portal;
    public List<Material> materials;
    
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F) && IsInDistance() && !isTriggered) || Input.GetKeyDown(cheatKey))
        {
            for (int i = 0; i < objectsToChangeMat.Count; i++)
            {
                objectsToChangeMat[i].GetComponent<Renderer>().material = materials[i];
            }
            
            isTriggered = true;
            pillar.EnableTotem();
            portal.SetActive(false);
            SoundManager.instance.PlaySound(SoundID.SUCCESS);
        }
    }

    bool IsInDistance()
    {
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
