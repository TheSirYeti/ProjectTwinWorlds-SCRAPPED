using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAttack : MonoBehaviour
{
    //[SerializeField] private float xMax, xMin;
    //[SerializeField] private float yMax, yMin;

    [SerializeField] private GameObject _rock;

    public void InstantiateAttack()
    {
        GameObject rock = Instantiate(_rock, new Vector3(Random.Range(PlayerWorlds.instance.currentPlayer.transform.position.x - 2, PlayerWorlds.instance.currentPlayer.transform.position.x + 2), 1, Random.Range(PlayerWorlds.instance.currentPlayer.transform.position.z - 2, PlayerWorlds.instance.currentPlayer.transform.position.z + 2)), Quaternion.identity);
        Destroy(rock, 1f);
    }
}
