using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class SwordSummoning : MonoBehaviour
{
    [SerializeField] private List<GameObject> swordSet;
    [SerializeField] private List<ParticleSystem> floorParticles;
    [SerializeField] private float waitTime = 3f;
    
    private int loopAmount;
    private int swordAmount;

    public void DoSwordElevation(int count, int loop)
    {
        swordAmount = count;
        loopAmount = loop;
        loopAmount--;
        List<int> swordsToElevate = new List<int>();
        
        for (int i = 0; i < count; i++)
        {
            int rand = UnityEngine.Random.Range(0, swordSet.Count);
            
            while (CheckIfInList(rand, swordsToElevate))
            {
                rand = UnityEngine.Random.Range(0, swordSet.Count);
            }
            
            swordsToElevate.Add(rand);
        }

        StartCoroutine(ElevateSword(swordsToElevate));
    }

    IEnumerator ElevateSword(List<int> ids)
    {
        foreach (var id in ids)
        {
            floorParticles[id].Play();
        }
        
        yield return new WaitForSeconds(waitTime);
        
        foreach (var id in ids)
        {
            floorParticles[id].Stop();
            swordSet[id].transform.position += Vector3.up * 3;
        }
        
        yield return new WaitForSeconds(waitTime);
        
        foreach (var id in ids)
        {
            swordSet[id].transform.position += Vector3.up * -3;
        }

        if (loopAmount > 0)
        {
            DoSwordElevation(swordAmount, loopAmount);
        }
        
        yield return new WaitForEndOfFrame();
    }

    bool CheckIfInList(int id, List<int> myList)
    {
        bool flag = false;

        foreach (int value in myList)
        {
            if (id == value)
                flag = true;
        }

        return flag;
    }
}
