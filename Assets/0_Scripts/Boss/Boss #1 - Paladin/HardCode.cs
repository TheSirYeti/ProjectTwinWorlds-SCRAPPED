using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCode : MonoBehaviour
{

    public PaladinLogic tp;

    
    void Update()
    {
        if (tp != null && tp.gameObject.activeSelf)
        {
            transform.position = tp.transform.position;

            if (!tp.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
        
        if(tp.currentPhase > 3)
            Destroy(gameObject);
    }
}
