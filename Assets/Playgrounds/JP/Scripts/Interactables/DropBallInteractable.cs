using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DropBallInteractable : BaseInteractable, IWeaponInteractable
{
    [SerializeField] private Rigidbody objectToDrop;
    
    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        _actualBullet = bullet;
        objectToDrop.useGravity = true;
        objectToDrop.constraints = RigidbodyConstraints.None;
        objectToDrop.transform.SetParent(null);
        transform.position += new Vector3(0, -1000, 0);
        bullet.transform.position += new Vector3(0, 1000, 0);
        bullet.Bullet_Reset();
        Destroy(objectToDrop, 5f);
        _isOnUse = false;

    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {
        //
    }

    public void Inter_ResetObject()
    {
        _isOnUse = false;
        //
    }

    public void Inter_SetParent(Transform weapon)
    {
        weapon.parent = transform;
        weapon.localScale = weapon.localScale;
        weapon.localPosition = Vector3.zero;
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        if (Vector3.Distance(actualPlayer.transform.position, transform.position) > _distanceToInteract) return false;

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

    public GameObject Inter_GetGameObject()
    {
        return gameObject;
    }
}
