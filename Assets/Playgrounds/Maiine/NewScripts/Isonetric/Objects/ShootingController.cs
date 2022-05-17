using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController
{
    bool isShooting = false;
    bool isConnect = false;

    bool _isDemon;

    Projectile _actualBullet = null;
    Player _myPlayer = null;

    LayerMask _usableItems;

    public ShootingController(Projectile myBullet, Player myPlayer, LayerMask usableItems, bool isDemon)
    {
        _myPlayer = myPlayer;
        _actualBullet = myBullet;
        _actualBullet.myShootingController = this;
        _usableItems = usableItems;
        _isDemon = isDemon;
    }

    public void SetConnectObject()
    {
        isConnect = true;
    }

    public void Shoot()
    {
        if (!isShooting)
        {
            _actualBullet.transform.position = _myPlayer.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                _actualBullet.StartShoot(hit.point);

            isShooting = true;
        }
        else if (isShooting && isConnect)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _usableItems))
            {
                hit.collider.gameObject.GetComponent<IWeaponInteractable>().DoWeaponAction(_myPlayer, _isDemon);
                _actualBullet.transform.parent = null;
                _actualBullet.transform.position = new Vector3(0, -50, 0);
                isShooting = false;
            }
        }
        else
        {
            _actualBullet.transform.parent = null;
            _actualBullet.transform.position = new Vector3(0, -50, 0);
            isShooting = false;
        }
    }
}
