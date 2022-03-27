using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour, ISubscriber
{
    public Animator animator;
    public Observer _playerObserver;


    private void Start()
    {
        _playerObserver.Subscribe(this);
    }

    public void OnNotify(string eventID)
    {
        if (eventID == "BasicAttack")
        {
            BasicAttack();
        }

        if (eventID == "Walking")
        {
            animator.SetBool("isWalking", true);
        }
        
        if (eventID == "Idle")
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void BasicAttack()
    {
        animator.Play("BasicAttack");
    }
}
