using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWorlds : MonoBehaviour
{
    public GameObject angelPlayer, demonPlayer;
    public bool isLinked;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            angelPlayer.SetActive(!angelPlayer.activeSelf);
            demonPlayer.SetActive(!demonPlayer.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            isLinked = !isLinked;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
