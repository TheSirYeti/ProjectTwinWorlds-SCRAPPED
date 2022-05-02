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
        Destroy(gameObject);
    }

    private void Update()
    {
        if (needsBothPlayers)
        {
            if (demon || angel)
            {
                oneReady.SetActive(true);
            } else oneReady.SetActive(false);
            
            if(demon && angel)
            {
                Destroy(PlayerWorlds.instance);
                LevelManager.instance.LoadNextScene(levelID);
                Debug.Log("CARGARON AMBOS!");
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            angel = true;
            
            if (!needsBothPlayers)
            {
                Destroy(PlayerWorlds.instance.angelPlayer);
                Destroy(PlayerWorlds.instance.demonPlayer);
                LevelManager.instance.LoadNextScene(levelID);
                Debug.Log("CARGO ANGEL!");
                Destroy(gameObject);
            }
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger"))
        {
            demon = true;
            
            if (!needsBothPlayers)
            {
                Destroy(PlayerWorlds.instance.angelPlayer);
                Destroy(PlayerWorlds.instance.demonPlayer);
                LevelManager.instance.LoadNextScene(levelID);
                Debug.Log("CARGO DEMONIO!");
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            angel = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger"))
        {
            demon = false;
        }
    }

}
