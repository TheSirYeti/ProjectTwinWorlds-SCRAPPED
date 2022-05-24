using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour, IPlayerInteractable, IWeaponInteractable
{
    public delegate void MovableBoxDelegate();
    MovableBoxDelegate actualMovement = delegate { };
    MovableBoxDelegate actualInteract = delegate { };

    bool isOnPlayer = false;
    bool _weaponIsHere = false;

    public float speed;
    public float maxForce;
    public float maxDistance;
    public float minDistance;
    public float aceleration;
    public float deaceleration;

    private float _actualForce;
    public bool isFollow;

    public float maxConnectDistance;

    Player _followPlayer;
    Projectile _actualWeapon;

    public GameObject goTo;

    public Rope ropePrefab;
    public Rope actualRope;

    private void Start()
    {
        actualInteract = StartAction;
    }

    void Update()
    {
        actualMovement();
    }

    #region Interaccion Por Cercania
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
    #endregion

    public void Interact(Player actualPlayer, bool isDemon, Projectile weapon, ShootingController shootingController)
    {
        if (!isDemon || Vector3.Distance(actualPlayer.transform.position, transform.position) > maxConnectDistance)
        {
            if (WeaponIsHere())
                shootingController.ResetShoot();
            return;
        }

        _followPlayer = actualPlayer;
        _actualWeapon = weapon;

        if (WeaponIsHere())
            actualInteract();
        else
            shootingController.ResetShoot();
    }

    public bool WeaponIsHere()
    {
        return _weaponIsHere;
    }

    public void SetWeaponState(bool state)
    {
        _weaponIsHere = state;
    }


    void StartAction()
    {
        actualMovement = ConnectingMovement;
        _actualWeapon.transform.parent = transform;
        actualInteract = StopAction;
    }

    void StopAction()
    {
        actualMovement = delegate { };
        _actualWeapon.transform.parent = null;
        actualInteract = StartAction;
    }

    #region Void de movimiento para Delegate
    void ConnectingMovement()
    {
        if (Vector3.Distance(transform.position, _followPlayer.transform.position) > maxDistance && !isFollow)
        {
            isFollow = true;
            actualMovement = FollowPlayer;
        }
    }

    void FollowPlayer()
    {
        transform.position += (_followPlayer.transform.position - transform.position) * _actualForce * Time.deltaTime;

        if (_actualForce < maxForce)
            _actualForce += aceleration * Time.deltaTime;

        if (Vector3.Distance(transform.position, _followPlayer.transform.position) < minDistance)
        {
            isFollow = false;
            actualMovement = Force;
        }
    }

    void Force()
    {
        transform.position += (_followPlayer.transform.position - transform.position) * _actualForce * Time.deltaTime;
        _actualForce -= deaceleration * Time.deltaTime;

        if (_actualForce <= 0)
            actualMovement = ConnectingMovement;
    }
    #endregion

}
