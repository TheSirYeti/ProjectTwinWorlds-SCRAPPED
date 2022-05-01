using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadLogic : MonoBehaviour
{
    public int levelID;
    public bool angel = false, demon = false;

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
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            demon = true;
        }

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
