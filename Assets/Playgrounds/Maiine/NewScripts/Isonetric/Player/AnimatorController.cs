using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController
{
    protected Animator _animator;

    public void ChangeBool(string boolName, bool state)
    {
        _animator.SetBool(boolName, state);
    }

    public void SetTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void SetFloat(string floatName, float value)
    {
        _animator.SetFloat(floatName, value);
    }

    public void SetInt(string intName, int value)
    {
        _animator.SetInteger(intName, value);
    }

    public bool CheckAnimationState(string animationName)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            return true;
        else
            return false;
    }
}
