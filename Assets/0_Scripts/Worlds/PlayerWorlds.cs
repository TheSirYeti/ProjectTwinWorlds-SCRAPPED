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
            demonPlayer.SetActive(!demonPlayer.activeSelf);
            angelPlayer.SetActive(!angelPlayer.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            isLinked = !isLinked;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        
        if (angelPlayer.activeSelf)
        {
            if (isLinked)
            {
                demonPlayer.transform.position = angelPlayer.transform.position;
            }
        }
        else
        {
            if (isLinked)
            {
                angelPlayer.transform.position = demonPlayer.transform.position;
            }
        }
    }
}
