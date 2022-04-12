using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<int> levelIds = new List<int>();
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentId = 0;
            ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentId = 1;
            ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentId = 2;
            ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentId = 3;
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
}
