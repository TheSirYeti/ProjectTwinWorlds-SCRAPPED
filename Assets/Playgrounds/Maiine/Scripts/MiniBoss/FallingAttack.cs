using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAttack : MonoBehaviour
{

    [SerializeField] private float xMax, xMin;
    [SerializeField] private float yMax, yMin;

    [SerializeField] private GameObject _rock;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void InstantiateAttack()
    {
        GameObject rock = Instantiate(_rock, new Vector3(Random.Range(xMin, xMax), 1, Random.Range(yMin, yMax)), Quaternion.identity);
        Destroy(rock, 3f);
    }
}
