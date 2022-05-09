using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PillarFall : MonoBehaviour
{
    public List<GameObject> objList;

    public GameObject preObj, afterObj;
    private bool flag;

    // Update is called once per frame
    void Update()
    {
        flag = true;
        foreach (GameObject gameObject in objList)
        {
            if (gameObject.activeSelf)
                flag = false;
        }

        if (flag)
        {
            preObj.SetActive(false);
            afterObj.SetActive(true);
            Destroy(gameObject);
        }
    }
}
