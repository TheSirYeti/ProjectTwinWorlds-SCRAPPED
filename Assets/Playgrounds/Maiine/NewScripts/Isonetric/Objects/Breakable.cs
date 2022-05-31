using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IWeaponInteractable
{
    public List<MeshDestroy> myMesh;
    public bool _usableByDemon;
    public bool _isConnect;

    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        bullet.Bullet_Reset();
        bullet.transform.parent = null;
        //Destroy(gameObject);
        foreach (var mesh in myMesh)
        {
            mesh.DestroyMesh();
        }
        gameObject.SetActive(false);
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {
    }

    public void Inter_ResetObject()
    {
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        if (_usableByDemon == isDemon)
        {
            return true;
        }
        else
            return false;
    }

    public bool Inter_OnUse()
    {
        return _isConnect;
    }

    public void Inter_SetParent(Transform weapon)
    {
        weapon.parent = transform;
        weapon.localScale = new Vector3(1, 1, 1);
        weapon.localPosition = Vector3.zero;
    }

    public GameObject Inter_GetGameObject()
    {
        return this.gameObject;
    }
}
