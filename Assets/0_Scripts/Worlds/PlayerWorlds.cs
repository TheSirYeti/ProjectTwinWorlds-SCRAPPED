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
    public Transform cameraPosDemon, cameraPosAngel, cameraPosition;
    public bool isLinked;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (angelPlayer.activeSelf)
            {
                demonPlayer.SetActive(true);
                cameraPosition.position = cameraPosDemon.position;
                angelPlayer.SetActive(false);

                if (!isLinked)
                {
                    demonTotem.SetActive(false);
                    angelTotem.SetActive(true);
                }

                EventManager.Trigger("OnPlayerChange", demonPlayer, isLinked);
                SoundManager.instance.PlaySound(SoundID.SWAP);
            }
            else
            {
                demonPlayer.SetActive(false);
                cameraPosition.position = cameraPosAngel.position;
                angelPlayer.SetActive(true);

                if (!isLinked)
                {
                    demonTotem.SetActive(true);
                    angelTotem.SetActive(false);
                }

                EventManager.Trigger("OnPlayerChange", angelPlayer, isLinked);
                SoundManager.instance.PlaySound(SoundID.SWAP_2);
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
            cameraPosition.position = cameraPosAngel.position;
            if (isLinked)
            {
                demonPlayer.transform.position = angelPlayer.transform.position;
            }
        }
        else
        {
            cameraPosition.position = cameraPosDemon.position;
            if (isLinked)
            {
                angelPlayer.transform.position = demonPlayer.transform.position;
            }
        }
        
        demonTotem.transform.position = demonPlayer.transform.position;
        angelTotem.transform.position = angelPlayer.transform.position;
    }
}
