using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCode : MonoBehaviour
{

    public Transform tp;

    
    void Update()
    {
        if (tp.gameObject.activeSelf)
        {
            transform.position = tp.position;

            if (!tp.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
