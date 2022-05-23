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
    public float maxForce;
    public float minDistance;
    public float aceleration;

    private float _actualForce;
    public bool isFollow;

    public float maxConnectDistance;

    Player followPlayer;

    public GameObject goTo;

    public Rope ropePrefab;
    public Rope actualRope;

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
        if (!isDemon || Vector3.Distance(actualPlayer.transform.position, transform.position) > maxConnectDistance)
        {
            isOnWeapon = !isOnWeapon;
            return;
        }

        if (!isOnWeapon)
        {
            actualMovement = ConnectingMovement;
            isOnWeapon = true;
            followPlayer = actualPlayer;
            //actualRope = Instantiate(ropePrefab, transform);
            //actualRope.OnStart(transform, actualPlayer.transform);
        }
        else
        {
            actualMovement = delegate { };
            isOnWeapon = false;

            /*if (actualRope != null)
                Destroy(actualRope.gameObject);*/
        }
    }

    public void DoConnectAction()
    {

    }

    void ConnectingMovement()
    {
        if (Vector3.Distance(transform.position, followPlayer.transform.position) > minDistance && !isFollow)
        {
            isFollow = true;
            actualMovement += FollowPlayer;
        }
    }

    void FollowPlayer()
    {
        transform.position += (followPlayer.transform.position - transform.position) * _actualForce * Time.deltaTime;

        if (_actualForce < maxForce)
            _actualForce += aceleration * Time.deltaTime;

        if (Vector3.Distance(transform.position, followPlayer.transform.position) < minDistance)
        {
            actualMovement -= FollowPlayer;
            isFollow = false;
            actualMovement += Force;
        }
    }

    void Force()
    {
        transform.position += (followPlayer.transform.position - transform.position) * _actualForce * Time.deltaTime;
        _actualForce -= aceleration * Time.deltaTime;

        if (_actualForce <= 0)
            actualMovement -= Force;
    }

}
