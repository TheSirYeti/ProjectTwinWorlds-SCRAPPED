using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpot : MonoBehaviour, IPlayerInteractable
{
    public Transform climbPoint;

    public void Inter_DoJumpAction(Player actualPlayer, bool isDemon)
    {
        if (!isDemon) return;


        if (actualPlayer.transform.position.y <= transform.position.y)
            actualPlayer.transform.position = climbPoint.position;
    }

    public void Inter_DoPlayerAction(Player actualPlayer, bool isDemon)
    {
    }
}
