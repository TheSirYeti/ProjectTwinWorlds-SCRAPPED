using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour, IWeaponInteractable
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
    BulletSystem _bullet;

    [SerializeField]
    LineRenderer _lineRenderer;

    public LayerMask breakableLayer;

    void Update()
    {
        actualMovement();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, breakableLayer))
        {
            Destroy(hit.collider.gameObject);
        }
    }

    #region Interfaces Con Arma
    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        _bullet = bullet;
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

    public void Inter_SetParent(Transform weapon)
    {
        weapon.parent = transform;
        weapon.localScale = new Vector3(1, 1, 1);
        weapon.localPosition = Vector3.zero;
    }

    public GameObject Inter_GetGameObject()
    {
        return this.gameObject;
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
            speed -= deaceleration * Time.deltaTime;
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
