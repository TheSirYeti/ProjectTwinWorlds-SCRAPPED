using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinBreachState : MonoBehaviour
{
    private FloorAttack floorAttack;
    private Animator animator;
    
    public void PrepareAttack()
    {
        animator.SetBool("isFloorAttackFinished", false);
    }
}
