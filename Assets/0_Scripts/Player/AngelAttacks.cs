using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAttacks : PlayerAttacks
{
    public override void GenerateBasicAttack()
    {
        cooldownTimer = attackCooldown;
        Debug.Log("ATAQUE DEMONIO");
        _playerObserver.NotifySubscribers("BasicAttack");
    }
}
