using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator _animator;
    public AnimatorController(Animator animator)
    {
        _animator = animator;
    }

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
}
