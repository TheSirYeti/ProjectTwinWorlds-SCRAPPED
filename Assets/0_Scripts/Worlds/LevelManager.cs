using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentId;
    public int loadingScene;
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
        {
            LoadNextScene(0);
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;

        EventManager.ResetEventDictionary();
        SoundManager.instance.StopAllMusic();
        SoundManager.instance.StopAllSounds();
        StartCoroutine(LoadSceneBuffer());
    }

    public void LoadNextScene(int sceneID)
    {
        currentId = sceneID;

        EventManager.ResetEventDictionary();
        SoundManager.instance.StopAllMusic();
        SoundManager.instance.StopAllSounds();
        StartCoroutine(LoadSceneBuffer());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneBuffer()
    {
        SceneManager.LoadScene(loadingScene);
        yield return new WaitForSeconds(0.3f);
        
        if(PlayerWorlds.instance != null)
            Destroy(PlayerWorlds.instance);
        
        yield return new WaitForSeconds(0.2f);
        
        SceneManager.LoadSceneAsync(currentId);
        yield return new WaitForSeconds(0.0001f);
    }
}
