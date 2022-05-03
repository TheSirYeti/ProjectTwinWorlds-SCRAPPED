using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public MusicID musicID;
    public bool loop;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayMusic(musicID, loop);
    }
}
