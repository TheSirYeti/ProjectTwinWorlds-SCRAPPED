using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPOS : MonoBehaviour
{
    public GameObject player1, player2;
    public GameObject tp1, tp2;

    private void Update()
    {
        tp1.transform.position = player1.transform.position;
        tp2.transform.position = player2.transform.position;
    }
}
