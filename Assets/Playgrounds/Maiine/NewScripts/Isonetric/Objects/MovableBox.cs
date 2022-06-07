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

    public float maxForce;
    public float actualForce;
    public float deacelerationForce;
    public Vector3 dirForce;

    public LayerMask _layerMask;

    public float maxConnectDistance;

    public LayerMask breakableLayer;
    public LayerMask floorLayer;

    void Update()
    {
        actualMovement();
        pullMovement();
        CheckBreackable();
        Force();
    }

    #region Interfaces Con Arma
    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        _actualBullet = bullet;
        _isOnUse = true;
        actualMovement = Delegate_FollowPlayer;
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {

        _isOnUse = true;
    }

    public void Inter_ResetObject()
    {
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
            if (hit.collider.tag != "Player" && !_isOnUse)
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

        Vector3 raycastZ = new Vector3(0, 0, dir.z);
        Vector3 raycastX = new Vector3(dir.x, 0, 0);

        if (Physics.Raycast(transform.position, raycastZ, 1f, _layerMask))
            dir.z = 0;

        if (Physics.Raycast(transform.position, raycastX, 1f, _layerMask))
            dir.x = 0;

        transform.position += dir * speed * Time.deltaTime;
    }

    void Delegate_Pull()
    {

    }

    void CheckBreackable()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 1f, floorLayer))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2, breakableLayer);

            foreach (Collider collider in hitColliders)
            {
                Destroy(collider.gameObject);
            }
        }
    }

    void Force()
    {
        if (Physics.Raycast(transform.position, dirForce, 1f, _layerMask))
            actualForce = 0;

        actualForce = Mathf.Clamp(actualForce, 0, maxForce);

        transform.position += dirForce * actualForce * Time.deltaTime;
        actualForce -= deacelerationForce * Time.deltaTime;
    }

    public void SetForce(Vector3 dir, float forcePorcent)
    {
        actualForce = maxForce * forcePorcent;
        dirForce = dir;
    }
}
