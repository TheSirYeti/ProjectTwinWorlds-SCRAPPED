using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadLogic : MonoBehaviour
{
    public int levelID;

    public void LoadLevel()
    {
        Debug.Log(levelID);
        LevelManager.instance.LoadNextScene(levelID);
        Destroy(gameObject);
    }

    private void Update()
    {
        /*if (needsBothPlayers)
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
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LevelManager.instance.LoadNextScene(levelID);
            Destroy(gameObject);
        }

    }

}
