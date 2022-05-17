using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController 
{
    bool isShooting = false;
    bool isConnect = false;

    Projectile _actualBullet = null;
    Player _myPlayer = null;

    public ShootingController(Projectile myBullet, Player myPlayer)
    {
        _myPlayer = myPlayer;
        _actualBullet = myBullet;
        _actualBullet.myShootingController = this;
    }

    public void SetConnectObject()
    {
        isConnect = true;
    }

    public void Shoot()
    {
        if (!isShooting)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                _actualBullet.StartShoot(hit.point);

            Debug.Log(hit.point);

            _actualBullet.transform.position = _myPlayer.transform.position;
            isShooting = true;
        }
        else if (isShooting && isConnect)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 10))
            {
                
            }
            else
            {
                _actualBullet.transform.position = new Vector3(0, -50, 0);
                isShooting = false;
            }
        }
        else
        {
            _actualBullet.transform.position = new Vector3(0, -50, 0);
            isShooting = false;
        }
    }
}
