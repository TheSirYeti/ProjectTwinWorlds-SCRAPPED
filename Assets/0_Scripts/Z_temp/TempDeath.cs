using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDeath : MonoBehaviour
{
    public GameObject objectToEnable;
    void Start()
    {
        EventManager.Subscribe("OnPlayerDeath", Death);
    }

    void Death(object[] parameters)
    {
        SoundManager.instance.StopAllMusic();
        SoundManager.instance.StopAllSounds();
        SoundManager.instance.PlayMusic(MusicID.SAD_SONG);
        objectToEnable.SetActive(true);
        Time.timeScale = 0f;
    }
}
