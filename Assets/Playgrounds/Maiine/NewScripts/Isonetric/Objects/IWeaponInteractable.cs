using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInteractable
{
    public void Interact(Player actualPlayer, bool isDemon, Projectile weapon, ShootingController shootingController);
    public bool WeaponIsHere();
    public void SetWeaponState(bool state);
}
