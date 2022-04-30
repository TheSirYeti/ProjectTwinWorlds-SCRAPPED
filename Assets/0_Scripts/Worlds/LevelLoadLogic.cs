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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") ||
            other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            Destroy(PlayerWorlds.instance);
            Debug.Log(levelID);
            LevelManager.instance.LoadNextScene(levelID);
        }
    }
}
