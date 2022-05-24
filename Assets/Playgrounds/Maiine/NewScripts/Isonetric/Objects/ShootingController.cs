using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController
{
    bool isOnPlayer = false;
    bool isOnItem = false;
    bool isConnected = false;

    bool _isDemon;

    Projectile _actualBullet = null;
    Player _myPlayer = null;
    IWeaponInteractable _actualInteractable = null;
    GameObject _actualObject = null;

    LayerMask _usableItems;

    public ShootingController(Projectile myBullet, Player myPlayer, LayerMask usableItems, bool isDemon)
    {
        _myPlayer = myPlayer;
        _actualBullet = myBullet;
        _actualBullet.myShootingController = this;
        _usableItems = usableItems;
        _isDemon = isDemon;
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            IWeaponInteractable actualInteractable = hit.collider.gameObject.GetComponent<IWeaponInteractable>();

            if (isOnPlayer)
            {
                _actualInteractable = actualInteractable;
                _actualObject = hit.collider.gameObject;
                _actualBullet.transform.position = _myPlayer.transform.position;
                _actualBullet.StartShoot(hit.point, hit.collider.gameObject);
                isOnPlayer = false;
            }
            else if (_actualObject == hit.collider.gameObject && actualInteractable != null)
            {
                actualInteractable.Interact(_myPlayer, _isDemon, _actualBullet, this);
            }
            else
            {
                ResetShoot();
            }
        }
    }

    public void SetConnectObject()
    {
        isOnItem = true;
    }

    public void ResetShoot()
    {
        isOnPlayer = true;
        isOnItem = false;
        _actualBullet.transform.parent = null;
        _actualBullet.transform.position = new Vector3(0, -50, 0);
        _actualBullet.StopShoot();
    }
}