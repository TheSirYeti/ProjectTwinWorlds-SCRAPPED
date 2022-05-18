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

    public float maxConnectDistance;

    Player followPlayer;

    public LineRenderer lineRenderer;

    public GameObject goTo;

    void Update()
    {
        actualMovement();
    }

    public void DoPlayerAction(Player actualPlayer, bool isDemon)
    {
        if (!isDemon) return;

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

    public void DoWeaponAction(Player actualPlayer, bool isDemon)
    {
        if (!isDemon || Vector3.Distance(actualPlayer.transform.position, transform.position) > maxConnectDistance) return;

        if (!isOnWeapon)
        {
            actualMovement = FollowPlayer;
            isOnWeapon = true;
            followPlayer = actualPlayer;
            lineRenderer.enabled = true;
        }
        else
        {
            actualMovement = delegate { };
            isOnWeapon = false;
            lineRenderer.enabled = false;
        }
    }

    public void DoConnectAction()
    {

    }

    void FollowPlayer()
    {
        Debug.Log("toy aca0");
        Vector3 maxPosition = (followPlayer.transform.position - transform.position);
        maxPosition.Normalize();

        Vector3 goToPoint = maxPosition * maxDist;
        goToPoint = transform.position + goToPoint;

        goTo.transform.position = goToPoint;

        transform.position += (goToPoint - transform.position) * speed * Time.deltaTime;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, followPlayer.transform.position);
    }

}
