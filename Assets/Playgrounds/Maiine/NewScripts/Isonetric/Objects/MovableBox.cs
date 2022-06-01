using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : BaseInteractable, IWeaponInteractable
{
    public delegate void MovableBoxDelegate();
    MovableBoxDelegate actualMovement = delegate { };
    MovableBoxDelegate pullMovement = delegate { };

    public float speed;
    public float maxSpeed;
    public float minDistaceToFollow;
    public float aceleration;
    public float deaceleration;

    public float maxConnectDistance;

    [SerializeField]
    LineRenderer _lineRenderer;

    public LayerMask breakableLayer;

    void Update()
    {
        actualMovement();
        pullMovement();
        CheckBreackable();
    }

    #region Interfaces Con Arma
    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        _actualBullet = bullet;
        _lineRenderer.enabled = true;
        _isOnUse = true;
        actualMovement = Delegate_FollowPlayer;
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {

        _isOnUse = true;
    }

    public void Inter_ResetObject()
    {
        _lineRenderer.enabled = false;
        actualMovement = delegate { };
        _isOnUse = false;
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        //Checkeo distancia
        if (Vector3.Distance(actualPlayer.transform.position, transform.position) > _distanceToInteract) return false;

        //Checkeo si no hay nada en medio
        Vector3 dir = actualPlayer.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir.normalized, out hit, Mathf.Infinity, _ignoreInteractableMask))
        {
            if (hit.collider.tag != "Player")
                return false;
        }

        if (_isUsableByDemon == isDemon)
        {
            _actualPlayer = actualPlayer;
            return true;
        }

        return false;
    }

    public bool Inter_OnUse()
    {
        return _isOnUse;
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
        if (Vector3.Distance(transform.position, _actualPlayer.transform.position) > minDistaceToFollow)
        {
            speed += aceleration * Time.deltaTime;
        }
        else
        {
            speed -= deaceleration * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        Vector3 dir = (_actualPlayer.transform.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _actualPlayer.transform.position);
    }

    void Delegate_Pull()
    {

    }

    void CheckBreackable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, breakableLayer))
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
