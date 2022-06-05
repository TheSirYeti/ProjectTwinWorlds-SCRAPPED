using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : BaseInteractable, IWeaponInteractable
{
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Vector3 rotationValues;
    
    public delegate void PlatformMovementDelegate();
    public PlatformMovementDelegate currentSpin = delegate {  };

    private void Update()
    {
        currentSpin();
    }

    #region Interfaz
    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        _actualBullet = bullet;
        currentSpin = SpinLogicDelegate;
        _isOnUse = true;
    }
    
    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {
        throw new System.NotImplementedException();
    }


    public void Inter_ResetObject()
    {
        currentSpin = delegate {  };
        _actualPlayer.myMovementController.ChangeToMove();
        _isOnUse = false;
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
        if (!Physics.Raycast(transform.position, dir,  dir.magnitude, _ignoreInteractableMask) && _isUsableByDemon == isDemon)
        {
            _actualPlayer = actualPlayer;
            _actualPlayer.myMovementController.ChangeToStay();
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
    #endregion

    void SpinLogicDelegate()
    {
        float h = Input.GetAxis("Horizontal");

        if (h != 0 && _isOnUse)
        {
            transform.Rotate(rotationValues * h * Time.deltaTime);
        }
    }
}
