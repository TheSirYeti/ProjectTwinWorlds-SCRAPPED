using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour, IPlayerInteractable, IWeaponInteractable
{
    public delegate void MovableBoxDelegate();
    MovableBoxDelegate actualMovement = delegate { };

    bool isOnPlayer = false;

    public float speed;
    public float maxSpeed;
    public float minDistaceToFollow;
    public float aceleration;
    public float deaceleration;

    [SerializeField]
    bool _isConnect;

    [SerializeField]
    bool _usableByDemon;

    public float maxConnectDistance;

    Player _followPlayer;

    [SerializeField]
    LineRenderer _lineRenderer;

    void Update()
    {
        actualMovement();
    }

    #region Interfaces Con Personaje
    public void Inter_DoPlayerAction(Player actualPlayer, bool isDemon)
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


    #region Interfaces Con Arma
    public void Inter_DoWeaponAction()
    {
        _lineRenderer.enabled = true;
        _isConnect = true;
        actualMovement = Delegate_FollowPlayer;
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {

        _isConnect = true;
    }

    public void Inter_ResetObject()
    {
        _lineRenderer.enabled = false;
        actualMovement = delegate { };
        _isConnect = false;
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        if (_usableByDemon == isDemon)
        {
            _followPlayer = actualPlayer;
            return true;
        }
        else
            return false;
    }

    public bool Inter_OnUse()
    {
        return _isConnect;
    }
    #endregion

    void Delegate_FollowPlayer()
    {
        if (Vector3.Distance(transform.position, _followPlayer.transform.position) > minDistaceToFollow)
        {
            speed += aceleration * Time.deltaTime;
        }
        else
        {
            speed += deaceleration * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        Vector3 dir = (_followPlayer.transform.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _followPlayer.transform.position);
    }


    void Delegate_Swing()
    {

    }
}
