using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public List<MarksController> marksToOpen;

    public List<bool> leverIsOn;
        
    void Start()
    {
        for (int i = 0; i < marksToOpen.Count; i++)
        {
            leverIsOn.Add(false);
        }
    }

    public void ActiveteTrigger()
    {
        for (int i = 0; i < leverIsOn.Count; i++)
        {
            if(leverIsOn[i] == false)
            {
                leverIsOn[i] = true;

                marksToOpen[i].ActiveMarks();

                if (CheckBools())
                    OpenDoor();

                break;
            }
        }
    }

    bool CheckBools()
    {
        for (int i = 0; i < leverIsOn.Count; i++)
        {
            if (leverIsOn[i] == false)
                return false;
        }
        return true;
    }

    void OpenDoor()
    {
        Destroy(gameObject);
    }
}
