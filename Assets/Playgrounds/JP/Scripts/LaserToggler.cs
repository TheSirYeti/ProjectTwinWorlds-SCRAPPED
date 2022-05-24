using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserToggler : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToEnable, objectsToDisable;
    [SerializeField] private Material goodMat, badMat;
    private bool isTriggered;

    public void SetToggleStatus(bool status)
    {
        isTriggered = status;

        if (isTriggered)
            GetComponent<Renderer>().material = goodMat;
        else GetComponent<Renderer>().material = badMat;
        
        foreach (var obj in objectsToEnable)
        {
            obj.SetActive(status);
        }

        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(!status);
        }
    }
}
