using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController
{
    bool isShooting = false;
    bool isConnect = false;
    bool isActive = false;

    bool _isDemon;

    Projectile _actualBullet = null;
    Player _myPlayer = null;
    IWeaponInteractable _actualInteractable = null;
    IWeaponInteractable _connectInteractable = null;
    GameObject _actualObject = null;
    GameObject _connectObject = null;

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
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (!isShooting)
            {
                _actualBullet.transform.position = _myPlayer.transform.position;

                _actualBullet.StartShoot(hit.point, hit.collider.gameObject);
                _actualInteractable = hit.collider.gameObject.GetComponent<IWeaponInteractable>();
                _actualObject = hit.collider.gameObject;

                isShooting = true;
            }
            else if (isConnect && !isActive)
            {
                _connectInteractable = hit.collider.gameObject.GetComponent<IWeaponInteractable>();
                if (hit.collider.gameObject == _actualObject)
                {
                    _actualInteractable.StartAction(_myPlayer, _isDemon, _actualBullet);
                    isActive = true;
                }
                else if (_connectInteractable != null)
                {
                    _connectObject = hit.collider.gameObject;
                    _connectInteractable.ConnectAction();
                    isActive = true;
                }
            }
            else if (isConnect && isActive)
            {
                if (hit.collider.gameObject == _actualObject || hit.collider.gameObject == _connectObject)
                {
                    if(_actualInteractable != null)
                    {
                        _actualInteractable.ResetAction();
                    }

                    if(_connectInteractable != null)
                    {
                        _connectInteractable.ResetAction();
                    }

                    _actualBullet.transform.parent = null;
                    _actualBullet.transform.position = new Vector3(0, -50, 0);
                    isActive = false;
                    isConnect = false;
                    isShooting = false;
                }
            }
            else if (isShooting && !isConnect && !isActive)
            {
                _actualBullet.transform.parent = null;
                _actualBullet.transform.position = new Vector3(0, -50, 0);
                _actualBullet.StopShoot();
                isShooting = false;
            }
        }
    }
}
