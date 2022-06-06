using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggle : MonoBehaviour
{
    private bool isDemon = false;
    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", ToggleMusic);
        
        SoundManager.instance.PlayMusic(MusicID.LVL1_AMBIENCE_ANGEL, true);
        SoundManager.instance.PlayMusic(MusicID.LVL1_AMBIENCE_DEMON, true);
        
        SoundManager.instance.PauseMusic(MusicID.LVL1_AMBIENCE_ANGEL);
        SoundManager.instance.PauseMusic(MusicID.LVL1_AMBIENCE_DEMON);
        
        ToggleMusic(null);
    }

    void ToggleMusic(object[] parameters)
    {
        isDemon = !isDemon;

        if (isDemon)
        {
            SoundManager.instance.PauseMusic(MusicID.LVL1_AMBIENCE_ANGEL);
            SoundManager.instance.ResumeMusic(MusicID.LVL1_AMBIENCE_DEMON);
        }
        else
        {
            SoundManager.instance.ResumeMusic(MusicID.LVL1_AMBIENCE_ANGEL);
            SoundManager.instance.PauseMusic(MusicID.LVL1_AMBIENCE_DEMON);
        }
    }
}
