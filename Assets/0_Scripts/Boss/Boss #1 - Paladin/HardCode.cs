using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCode : MonoBehaviour
{

    public Transform tp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
