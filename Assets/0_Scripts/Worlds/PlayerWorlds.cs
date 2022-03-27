using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerWorlds : MonoBehaviour
{
    public GameObject angelPlayer, demonPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            angelPlayer.SetActive(!angelPlayer.activeSelf);
            demonPlayer.SetActive(!demonPlayer.activeSelf);
        }
    }
}
