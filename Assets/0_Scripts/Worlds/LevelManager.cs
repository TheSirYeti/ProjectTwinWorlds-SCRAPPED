using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentId;
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f; 
        EventManager.ResetEventDictionary();
        SoundManager.instance.StopAllMusic();
        SoundManager.instance.StopAllSounds();
        SceneManager.LoadScene(currentId);
    }

    public void LoadNextScene(SceneID sceneID)
    {
        currentId = (int) sceneID;
        SceneManager.LoadSceneAsync((int) sceneID);
    }
    
    public enum SceneID
    {
        MAIN_MENU,
        LEVEL_1,
        LEVEL_2,
        BOSS,
        BOSS_2,
        MINIBOSS1
    }
}
