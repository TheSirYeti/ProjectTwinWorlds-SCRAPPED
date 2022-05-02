using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWorlds : MonoBehaviour
{
    public GameObject angelPlayer, demonPlayer, currentPlayer, firstTriggerPlayer;
    public GameObject angelTotem, demonTotem;
    public bool isShooting;
    public Cinemachine.CinemachineVirtualCamera vCamera;
    
    public bool isLinked;

    public static PlayerWorlds instance;

    private void Awake()
    {
        instance = this;

        currentPlayer = demonPlayer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isShooting) 
        {
            if (angelPlayer.activeSelf)
            {
                demonPlayer.SetActive(true);
                demonTotem.SetActive(false);
                angelPlayer.SetActive(false);
                angelTotem.SetActive(true);
                vCamera.Follow = demonPlayer.transform;
                currentPlayer = demonPlayer;
                EventManager.Trigger("OnPlayerChange", demonPlayer, isLinked);
            }
            else
            {
                //demonWorld.SetActive(false);
                demonPlayer.SetActive(false);
                demonTotem.SetActive(true);
                angelPlayer.SetActive(true);
                //angelWorld.SetActive(true);
                angelTotem.SetActive(false);
                vCamera.Follow = angelPlayer.transform;
                currentPlayer = angelPlayer;
                EventManager.Trigger("OnPlayerChange", angelPlayer, isLinked);
            }
            SoundManager.instance.PlaySound(SoundID.SWAP);
        }

        demonTotem.transform.position = demonPlayer.transform.position;
        angelTotem.transform.position = angelPlayer.transform.position;
    }
}
