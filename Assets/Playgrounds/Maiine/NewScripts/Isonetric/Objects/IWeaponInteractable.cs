using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInteractable
{
    public void DoWeaponAction(Player actualPlayer, bool isDemon);
    public void DoConnectAction();
}
