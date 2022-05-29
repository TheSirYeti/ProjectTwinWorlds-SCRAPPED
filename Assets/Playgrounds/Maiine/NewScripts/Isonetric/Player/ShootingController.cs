using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController
{
    BulletSystem _actualBullet = null;
    LayerMask _posibleCollisions;

    public ShootingController(BulletSystem myBullet, LayerMask posibleCollisions)
    {
        _actualBullet = myBullet;
        _posibleCollisions = posibleCollisions;
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _posibleCollisions))
        {
            IWeaponInteractable actualInteractable = hit.collider.gameObject.GetComponent<IWeaponInteractable>();

            _actualBullet.Bullet_Shoot(hit.point, actualInteractable);
        }
    }
}