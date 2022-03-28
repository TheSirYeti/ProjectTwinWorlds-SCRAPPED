using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool isEnabled;

    public abstract void EnableInteractable();
    
    public abstract void DisableInteractable();
}
