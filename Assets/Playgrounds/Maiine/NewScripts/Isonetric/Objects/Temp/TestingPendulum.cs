using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPendulum : MonoBehaviour
{
    public float maxAngleDeflection = 0f;
    public float upgradeSpeed;
    public float decreaseSpeed;
    public float speedOfPendulum = 1.0f;

    public Transform tempPlayer;
    public Transform midPos;
    public Transform actualLook = null, rigthLook, leftLook;

    public LayerMask collisionFloor;

    public int actualDir = 0;

    private void Start()
    {
        //Vector3 mid = midPos.position - transform.position;
        //Vector3 playe = tempPlayer.position - transform.position;

        //maxAngleDeflection = Vector3.Angle(mid, playe);
        //transform.localRotation = Quaternion.Euler(0, 0, maxAngleDeflection);

        //Debug.Log(Vector3.Angle(mid, playe));
    }

    void Update()
    {
        float angle = maxAngleDeflection * Mathf.Sin(Time.time * speedOfPendulum);
        transform.localRotation = Quaternion.Euler(0, 0, angle);


        if (maxAngleDeflection == 0)
            actualDir = 0;
        else if (maxAngleDeflection - Mathf.Abs(angle) < maxAngleDeflection * 0.02f)
        {
            if (angle > 0)
            {
                actualLook = leftLook;
                actualDir = 2;
            }
            else if (angle < 0)
            {
                actualLook = rigthLook;
                actualDir = 1;
            }
        }


        CheckInitialDir();


        if (actualDir == 1)
        {
            if (Input.GetKey(KeyCode.D))
                maxAngleDeflection += upgradeSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.A))
                maxAngleDeflection -= upgradeSpeed * 0.8f * Time.deltaTime;
        }
        else if (actualDir == 2)
        {
            if (Input.GetKey(KeyCode.A))
                maxAngleDeflection += upgradeSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.D))
                maxAngleDeflection -= upgradeSpeed * 0.8f * Time.deltaTime;
        }


        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            maxAngleDeflection -= decreaseSpeed * Time.deltaTime;
        }

        tempPlayer.forward = tempPlayer.position - actualLook.position;

        if (!Physics.Raycast(tempPlayer.position, Vector3.down, 0.5f, collisionFloor))
            maxAngleDeflection = Mathf.Clamp(maxAngleDeflection, 0, 90);
    }

    void CheckInitialDir()
    {
        if (actualDir == 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                actualDir = 1;
                actualLook = rigthLook;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                actualDir = 2;
                actualLook = leftLook;
            }
        }
    }
}
