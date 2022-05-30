using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour, IPlayerInteractable
{
    public DoorController myDoor;
    public bool isTrigger = false;
    public float seeTime;

    public void Inter_DoJumpAction(Player actualPlayer, bool isDemon){}

    public void Inter_DoPlayerAction(Player actualPlayer, bool isDemon)
    {
        if (!isTrigger)
        {
            EventManager.Trigger("SeeObject", myDoor.gameObject.transform.position, seeTime);
            myDoor.ActiveteTrigger();
            isTrigger = true;
        }
    }
}
