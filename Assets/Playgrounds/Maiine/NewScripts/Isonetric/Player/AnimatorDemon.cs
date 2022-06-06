using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDemon : AnimatorController
{
    public AnimatorDemon(Animator animator)
    {
        _animator = animator;
    }

    public void SetWalkValue(float magnitud)
    {
        _animator.SetFloat("MoveMagnitud", magnitud);
    }

    public void SetAim(bool aimState)
    {
        _animator.SetBool("IsAiming", aimState);
    }

    public void TriggerShoot()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Ani_AimIdle"))
        {
            _animator.SetTrigger("Shoot");
        }
    }
}
