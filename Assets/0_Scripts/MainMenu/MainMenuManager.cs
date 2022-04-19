using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject menuReal, menuMeme;

    private void Start()
    {
        SoundManager.instance.PlayMusic(MusicID.MAIN_MENU);
    }

    public void EnablePanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    
    public void DisablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    
    public void LoadScene(int id)
    {
        LevelManager.instance.LoadNextScene(id);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
