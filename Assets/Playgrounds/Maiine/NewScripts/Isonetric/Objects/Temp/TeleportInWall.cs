using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInWall : MonoBehaviour, IWeaponInteractable
{
    public Transform _pointToGo;
    Player _actualPlayer;
    BulletSystem actualBullet;

    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        Debug.Log("A");
        _actualPlayer.transform.position = _pointToGo.position;
        actualBullet = bullet;
        actualBullet.Bullet_Reset();
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {
        Debug.Log("V");
        otherObject.Inter_GetGameObject().transform.position = _pointToGo.position;
        _actualPlayer.myShootingController._actualBullet.Bullet_Reset();
    }

    public void Inter_ResetObject()
    {
        Debug.Log("D");
        _actualPlayer.myShootingController._actualBullet.transform.parent = null;
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        if (isDemon) return false;

        _actualPlayer = actualPlayer;
        return true;
    }

    public bool Inter_OnUse()
    {
        return false;
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
