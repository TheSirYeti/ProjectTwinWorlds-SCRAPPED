using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadLogic : MonoBehaviour
{
    public int levelID;
    bool angel = false, demon = false;
    public bool needsBothPlayers;
    public GameObject oneReady;

    public void LoadLevel()
    {
        Debug.Log(levelID);
        LevelManager.instance.LoadNextScene(levelID);
    }

    private void Update()
    {
        if (needsBothPlayers)
        {
            if (demon || angel)
            {
                oneReady.SetActive(true);
            } else oneReady.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            angel = true;
            
            if (!needsBothPlayers)
            {
                Destroy(PlayerWorlds.instance.angelPlayer);
                Destroy(PlayerWorlds.instance.demonPlayer);
                LevelManager.instance.LoadNextScene(levelID);
            }
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            demon = true;
            
            if (!needsBothPlayers)
            {
                Destroy(PlayerWorlds.instance.angelPlayer);
                Destroy(PlayerWorlds.instance.demonPlayer);
                LevelManager.instance.LoadNextScene(levelID);
            }
        }
        
        if(demon && angel)
        {
            Destroy(PlayerWorlds.instance);
            LevelManager.instance.LoadNextScene(levelID);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            angel = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            demon = false;
        }
    }

}
