using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public List<Interactable> interactables;

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
            gameObject.SetActive(false);
        }
    }
}
