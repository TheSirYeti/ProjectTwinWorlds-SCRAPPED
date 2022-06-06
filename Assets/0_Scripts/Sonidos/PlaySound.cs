using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    
    public void DoSound(SoundID soundID)
    {
        SoundManager.instance.PlaySound(soundID);
    }
}
