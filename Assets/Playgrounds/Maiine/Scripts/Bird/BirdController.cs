using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public List<GameObject> eatPoints;
    public int nextPoint;
    public Transform nextTarget;

    delegate void Accion();
    Accion actualAccion;

    public float speed;

    public GameObject door;

    private void Start()
    {
        actualAccion = EatWait;
    }

    void Update()
    {
        actualAccion();
    }

    public void OpenDoor()
    {
        SetNextTarget();
    }

    public void SetNextTarget()
    {
        if(nextPoint < 3)
        {
            nextPoint++;
            nextTarget = eatPoints[nextPoint].transform;
            actualAccion = GoTo;
        }
        else
        {
            Destroy(door);
        }
    }

    public void ResetAndBack()
    {
        Debug.Log("Reset");
        nextPoint = 0;
        nextTarget = eatPoints[nextPoint].transform;
        actualAccion = Back;
    }

    public void Back()
    {
        Debug.Log("volviendo");
        Vector3 dir = nextTarget.position - transform.position;
        transform.forward = dir;
        transform.position += transform.forward * Time.deltaTime * speed;
        if (dir.magnitude < 0.3)
            actualAccion = EatWait;
    }

    public void GoTo()
    {
        Vector3 dir = nextTarget.position - transform.position;
        transform.forward = dir;
        transform.position += transform.forward * Time.deltaTime * speed;

        if(dir.magnitude < 0.3)
        {

            if (eatPoints[nextPoint].GetComponent<FederController>().HaveFood())
            {
                Debug.Log("hay comida");
                actualAccion = EatWait;
                StartCoroutine(Wait());
            }
            else
            {
                Debug.Log("xddd");
                ResetAndBack();
            }
        }
    }
    
    public void EatWait()
    {

    }

    IEnumerator Wait()
    {
        Debug.Log("Corrutina");
        yield return new WaitForSeconds(2f);
        SetNextTarget();
    }

}
