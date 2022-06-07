using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SuperTemp2 : MonoBehaviour
{
    private bool isSpinning;
    private bool canPress;
    public GameObject objectToReplicate;
    public Transform myPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canPress)
        {
            Debug.Log("hit");
            StartCoroutine(DoSpin());
            GameObject obj = Instantiate(objectToReplicate);
            obj.transform.position = myPosition.position;
        }

        if (isSpinning)
        {
            transform.Rotate(new Vector3(0f, 5f, 0f));
        }
    }

    IEnumerator DoSpin()
    {
        isSpinning = true;
        yield return new WaitForSeconds(2f);
        isSpinning = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canPress = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canPress = false;
        }
    }
}
