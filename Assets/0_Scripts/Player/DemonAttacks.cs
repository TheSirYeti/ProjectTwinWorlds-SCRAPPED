using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAttacks : PlayerAttacks
{
    public override void GenerateBasicAttack()
    {
        cooldownTimer = attackCooldown;
        Debug.Log("ATAQUE DEMONIO");
        _playerObserver.NotifySubscribers("BasicAttack");
    }
}
