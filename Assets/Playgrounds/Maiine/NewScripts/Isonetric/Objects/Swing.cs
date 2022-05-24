using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour, IWeaponInteractable
{
    Player _player;
    GameObject _hangItem;

    public LineRenderer lineRenderer;

    bool _isActive;
    bool isOnWeapon;

    delegate void SwingDelegate();
    SwingDelegate actualDelegate = delegate { };

    void Start()
    {

    }

    void Update()
    {
        actualDelegate();
    }

    public void Interact(Player actualPlayer, bool isDemon, Projectile weapon, ShootingController shootingController)
    {
        throw new System.NotImplementedException();
    }

    public bool WeaponIsHere()
    {
        throw new System.NotImplementedException();
    }

    public void SetWeaponState(bool state)
    {
        throw new System.NotImplementedException();
    }
}
