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
    public GameObject angelWorld, demonWorld;
    public Cinemachine.CinemachineVirtualCamera vCamera;
    
    public bool isLinked;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            if (angelPlayer.activeSelf)
            {
                demonPlayer.SetActive(true);
                demonWorld.SetActive(true);
                angelPlayer.SetActive(false);
                angelWorld.SetActive(false);
                vCamera.Follow = demonPlayer.transform;
                EventManager.Trigger("OnPlayerChange", demonPlayer, isLinked);
            }
            else
            {
                demonWorld.SetActive(false);
                demonPlayer.SetActive(false);
                angelPlayer.SetActive(true);
                angelWorld.SetActive(true);
                vCamera.Follow = angelPlayer.transform;
                EventManager.Trigger("OnPlayerChange", angelPlayer, isLinked);
            }
            SoundManager.instance.PlaySound(SoundID.SWAP);
        }

        demonTotem.transform.position = demonPlayer.transform.position;
        angelTotem.transform.position = angelPlayer.transform.position;
    }
}
