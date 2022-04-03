using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class FloorAttack : MonoBehaviour
{
    public Animator animator;

    public void DoAttack()
    {
        animator.SetBool("canAttack", true);
    }

    public void StopAttack()
    {
        animator.SetBool("canAttack", false);
    }
}
