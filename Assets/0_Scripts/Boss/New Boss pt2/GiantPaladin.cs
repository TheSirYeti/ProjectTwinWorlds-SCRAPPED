using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantPaladin : MonoBehaviour
{
    public int hp;

    public GameObject handDropPrefab;
    public List<Transform> handDropPoints;

    public GameObject handSweepPrefab;
    public List<Transform> handSweepPoints;
    
    
    
    void DoHandDrop()
    {
        int rand = Random.Range(0, handDropPoints.Count);

        GameObject hand = Instantiate(handDropPrefab);
        hand.transform.position = handDropPoints[rand].transform.position;
    }
    
    void DoHandSweep()
    {
        int rand = Random.Range(0, handDropPoints.Count);

        GameObject hand = Instantiate(handDropPrefab);
        hand.transform.position = handDropPoints[rand].transform.position;
    }
}
