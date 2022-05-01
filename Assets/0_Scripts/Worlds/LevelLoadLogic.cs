using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadLogic : MonoBehaviour
{
    public int levelID;
    bool angel = false, demon = false;

    public void LoadLevel()
    {
        Debug.Log(levelID);
        LevelManager.instance.LoadNextScene(levelID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            angel = true;
            Debug.Log("ANGEL = " + true);
        }
        
        else if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            demon = true;
            Debug.Log("DEMON = " + true);
        }

        Debug.Log("ANGEL = " + true + ", DEMON =" + true);
        
        if(demon && angel)
        {
            Destroy(PlayerWorlds.instance);
            Debug.Log(levelID);
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
