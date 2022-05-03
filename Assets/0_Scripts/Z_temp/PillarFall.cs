using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PillarFall : MonoBehaviour
{
    

    public GameObject obj1, obj2;

    public GameObject preObj, afterObj;


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
