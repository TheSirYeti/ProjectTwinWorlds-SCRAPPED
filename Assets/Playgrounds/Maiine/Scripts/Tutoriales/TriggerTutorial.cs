using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    public TutorialController controller;
    public int id;
    public bool isForDemon, isForAngel;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DemonAttacks>())
        {
            if (isForDemon)
            {
                controller.SetTutorial(id);
            }
            else
            {
                controller.DisableTutorial();
            }
        }
        
        else if(other.GetComponent<AngelAttacks>())
        {
            if (isForAngel)
            {
                controller.SetTutorial(id);
            }
            else
            {
                controller.DisableTutorial();
            }
        }
    }
}
