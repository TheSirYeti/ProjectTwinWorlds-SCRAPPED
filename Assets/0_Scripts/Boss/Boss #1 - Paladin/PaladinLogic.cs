using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinLogic : MonoBehaviour
{
    public ShieldStruct shieldRing1, shieldRing2, shieldRing3;

    public void StartRingPhase(int phaseID)
    {
        switch (phaseID)
        {
            case 1:
                EnableRings(shieldRing1.shields);
                break;
            case 2:
                EnableRings(shieldRing1.shields);
                EnableRings(shieldRing2.shields);
                break;
            case 3:
                EnableRings(shieldRing1.shields);
                EnableRings(shieldRing2.shields);
                EnableRings(shieldRing3.shields);
                break;
        }
    }
    public void EnableRings(List<GameObject> rings)
    {
        foreach (GameObject gameObj in rings)
        {
            gameObj.SetActive(true);
        }
    }
}
