using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour, IWeaponInteractable
{
    Player _player;
    GameObject _hangItem;

    public LineRenderer lineRenderer;

    bool _isActive;

    delegate void SwingDelegate();
    SwingDelegate actualDelegate = delegate { };

    void Start()
    {
        
    }

    void Update()
    {
        actualDelegate();
    }

    public void DoWeaponAction(Player actualPlayer, bool isDemon)
    {
        if (isDemon) return;

        _player = actualPlayer;
    }

    public void DoConnectAction()
    {
        
    }

}
