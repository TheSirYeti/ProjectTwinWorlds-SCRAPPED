using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInteractable
{
    public void Inter_DoWeaponAction();
    public void Inter_DoConnectAction(IWeaponInteractable otherObject);
    public void Inter_ResetObject();
    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon);
    public bool Inter_OnUse();
}
