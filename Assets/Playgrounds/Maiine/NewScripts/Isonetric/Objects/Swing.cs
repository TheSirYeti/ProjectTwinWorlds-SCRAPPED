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

    public void StartAction(Player actualPlayer, bool isDemon, Projectile weapon)
    {
        if (isDemon)
        {
            isOnWeapon = !isOnWeapon;
            return;
        }

        _player = actualPlayer;
        
    }

    public void ConnectAction()
    {

    }

    public void ResetAction()
    {
        
    }
}
