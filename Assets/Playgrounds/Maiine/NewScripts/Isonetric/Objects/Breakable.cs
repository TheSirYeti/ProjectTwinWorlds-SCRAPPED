using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IWeaponInteractable
{
    bool _weaponHere;

    public List<MeshDestroy> myMesh;


    public void Interact(Player actualPlayer, bool isDemon, Projectile weapon, ShootingController shootingController)
    {
        if (!isDemon)
        {
            Restart();
            return;
        }

        if (WeaponIsHere())
            Break();
    }

    public void SetWeaponState(bool state)
    {
        _weaponHere = state;
    }

    public bool WeaponIsHere()
    {
        return _weaponHere;
    }

    void Break()
    {
        Destroy(gameObject);
        Restart();
    }

    void Restart()
    {
        
    }
}
