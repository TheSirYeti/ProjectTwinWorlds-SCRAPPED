using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInteractable
{
    public void Inter_DoWeaponAction(BulletSystem bullet);
    public void Inter_DoConnectAction(IWeaponInteractable otherObject);
    public void Inter_ResetObject();
    public void Inter_SetParent(Transform weapon);
    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon);
    public bool Inter_OnUse();
}
