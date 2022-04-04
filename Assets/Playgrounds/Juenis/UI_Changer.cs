using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Changer : MonoBehaviour
{
    public GameObject demonActive, angelActive;
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            
            demonActive.SetActive(!demonActive.activeSelf);
            angelActive.SetActive(!angelActive.activeSelf);
        }
    }
}
