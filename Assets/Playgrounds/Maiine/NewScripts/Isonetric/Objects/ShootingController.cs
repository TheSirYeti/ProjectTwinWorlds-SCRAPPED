using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    bool isShooting = false;

    Projectile _actualBullet = null;
    Player _myPlayer = null;

    public ShootingController(Projectile myBullet, Player myPlayer)
    {
        _myPlayer = myPlayer;
        //_actualBullet = Instantiate(myBullet, new Vector3(0, -50, 0), Quaternion.identity);
    }

    public void Shoot()
    {
        if (!isShooting)
        {
            _actualBullet.transform.position = _myPlayer.transform.position;
            isShooting = true;
        }
        else
        {
            _actualBullet.transform.position = new Vector3(0, -50, 0);
            isShooting = false;
        }
    }
}
