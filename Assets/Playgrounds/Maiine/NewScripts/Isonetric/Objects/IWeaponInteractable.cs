using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInteractable
{
    public void StartAction(Player actualPlayer, bool isDemon, Projectile weapon);
    public void ConnectAction();
    public void ResetAction();
}
