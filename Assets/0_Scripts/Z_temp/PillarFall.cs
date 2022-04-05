using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PillarFall : MonoBehaviour
{
    

    public GameObject obj1, obj2;

    public GameObject preObj, afterObj;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayMusic(MusicID.CHILLING_1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!obj1.activeSelf && !obj2.activeSelf)
        {
            preObj.SetActive(false);
            afterObj.SetActive(true);
        }
    }
}
