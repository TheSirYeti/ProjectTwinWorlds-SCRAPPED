using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour, IPlayerInteractable, IWeaponInteractable
{
    public delegate void MovableDelegate();
    MovableDelegate actualMovement = delegate { };

    bool isOnPlayer = false;
    bool isOnWeapon = false;

    public float speed;
    public float maxDist;
    public float minDist;

    Player followPlayer;

    void Update()
    {
        actualMovement();
    }

    public void DoPlayerAction(Player actualPlayer)
    {
        if (!isOnPlayer)
        {
            transform.parent = actualPlayer.gameObject.transform;
            isOnPlayer = true;
        }
        else
        {
            transform.parent = null;
            isOnPlayer = false;
        }
    }

    public void DoWeaponAction(Player actualPlayer)
    {
        if (!isOnWeapon)
        {
            actualMovement = FollowPlayer;
            isOnWeapon = true;
            followPlayer = actualPlayer;
        }
        else
        {
            actualMovement = delegate { };
            isOnWeapon = false;
        }
    }

    void FollowPlayer()
    {
        if (Vector3.Distance(followPlayer.transform.position, transform.position) > maxDist)
            transform.position += (followPlayer.transform.position - transform.position) * speed * Time.deltaTime;
    }
}
