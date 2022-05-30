using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorVFX : MonoBehaviour
{
    public List<ParticleSystem> allParticles;
    public Animator myAnimator;

    private void Start()
    {
        EventManager.Subscribe("OnOpenDoor", DoOpenTrigger);
    }

    public void DoOpenTrigger(object[] parameters)
    {
        myAnimator.Play("GoBack");

        foreach (var particle in allParticles)
        {
            particle.Play();
        }
    }

    public void StopVFX()
    {
        foreach (var particle in allParticles)
        {
            particle.Stop();
        }
    }
}
