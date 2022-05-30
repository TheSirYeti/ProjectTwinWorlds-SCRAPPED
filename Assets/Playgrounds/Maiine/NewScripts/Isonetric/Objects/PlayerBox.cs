using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBox : MonoBehaviour, IPlayerInteractable
{
    bool isOnPlayer = false;

    public Transform myBox;
    public Transform climbPoint;

    public void Inter_DoJumpAction(Player actualPlayer, bool isDemon)
    {
        if (!isDemon) return;


        if (actualPlayer.transform.position.y <= transform.position.y)
            actualPlayer.transform.position = climbPoint.position;
    }

    public void Inter_DoPlayerAction(Player actualPlayer, bool isDemon)
    {
        if (!isDemon) return;

        if (!isOnPlayer)
        {
            myBox.transform.parent = actualPlayer.gameObject.transform;
            isOnPlayer = true;
        }
        else
        {
            myBox.transform.parent = null;
            isOnPlayer = false;
        }
    }

}
