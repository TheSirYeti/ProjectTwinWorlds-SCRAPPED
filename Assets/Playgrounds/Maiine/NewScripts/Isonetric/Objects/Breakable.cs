using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IWeaponInteractable
{
    bool _weaponHere;

    public List<MeshDestroy> myMesh;

    public void Inter_DoWeaponAction()
    {
        throw new System.NotImplementedException();
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {
        throw new System.NotImplementedException();
    }

    public void Inter_ResetObject()
    {
        throw new System.NotImplementedException();
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        throw new System.NotImplementedException();
    }

    public bool Inter_OnUse()
    {
        throw new System.NotImplementedException();
    }

    public void Inter_SetParent(Transform weapon)
    {
        weapon.parent = transform;
        weapon.localScale = new Vector3(1, 1, 1);
        weapon.localPosition = Vector3.zero;
    }
}
