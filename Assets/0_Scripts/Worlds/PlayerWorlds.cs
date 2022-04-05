using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWorlds : MonoBehaviour
{
    public GameObject angelPlayer, demonPlayer;
    public GameObject angelTotem, demonTotem;
    public bool isLinked;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (angelPlayer.activeSelf)
            {
                demonPlayer.SetActive(true);
                angelPlayer.SetActive(false);

                if (!isLinked)
                {
                    demonTotem.SetActive(false);
                    angelTotem.SetActive(true);
                }

                EventManager.Trigger("OnPlayerChange", demonPlayer);
            }
            else
            {
                demonPlayer.SetActive(false);
                angelPlayer.SetActive(true);

                if (!isLinked)
                {
                    demonTotem.SetActive(true);
                    angelTotem.SetActive(false);
                }

                EventManager.Trigger("OnPlayerChange", angelPlayer);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            isLinked = !isLinked;
            
            if (isLinked)
            {
                demonTotem.SetActive(false);
                angelTotem.SetActive(false);
            }
            else if (angelPlayer.activeSelf)
            {
                demonTotem.SetActive(true);
                angelTotem.SetActive(false);
            }
            else
            {
                demonTotem.SetActive(false);
                angelTotem.SetActive(true);
            }
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
        
        demonTotem.transform.position = demonPlayer.transform.position;
        angelTotem.transform.position = angelPlayer.transform.position;
    }
}
