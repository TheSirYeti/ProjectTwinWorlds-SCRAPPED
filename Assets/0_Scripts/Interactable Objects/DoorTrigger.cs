using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public List<Interactable> interactables;
    public Transform finalPos, initialPos;
    
    public void CheckInteractableStatus()
    {
        bool flag = false;
        foreach (Interactable interactable in interactables)
        {
            if (interactable.isEnabled == false)
            {
                flag = true;
            }
        }

        if (!flag)
        {
            transform.position = finalPos.position;
        }
        else
        {
            transform.position = initialPos.position;
        }
    }
}
