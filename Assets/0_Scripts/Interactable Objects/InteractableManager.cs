using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager instance;

    public InteractableObject[] allInteractables;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < allInteractables.Length; i++)
        {
            allInteractables[i].id = i;
        }
    }

    public void ExecuteEnd(int objectID)
    {
        foreach (InteractableObject intObj in allInteractables)
        {
            if (intObj.gameObject.activeSelf && objectID == intObj.id)
            {
                Debug.Log("Lo hago ya ya ya ya ya");
                intObj.OnObjectEnd();
            }
        }
    }
}
