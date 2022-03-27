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

    public override void AimAbility()
    {
        throw new System.NotImplementedException();
    }

    public override void ThrowAbility()
    {
        throw new System.NotImplementedException();
    }
}
