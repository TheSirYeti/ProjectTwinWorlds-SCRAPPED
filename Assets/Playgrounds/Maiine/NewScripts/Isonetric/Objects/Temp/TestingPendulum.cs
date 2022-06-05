using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPendulum : MonoBehaviour
{
    public float MaxAngleDeflection = 0f;
    public float upgradeSpeed;
    public float decreaseSpeed;
    public float SpeedOfPendulum = 1.0f;

    public int actualDir = 0;

    void Update()
    {
        float angle = MaxAngleDeflection * Mathf.Sin(Time.time * SpeedOfPendulum);
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        Debug.Log(angle);

        CheckInitialDir();

        if (Input.GetKey(KeyCode.D))
            MaxAngleDeflection += upgradeSpeed * Time.deltaTime;

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            MaxAngleDeflection -= decreaseSpeed * Time.deltaTime;
        }

        MaxAngleDeflection = Mathf.Clamp(MaxAngleDeflection, 0, 90);
    }

    void CheckInitialDir()
    {
        if (actualDir == 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                actualDir = 1;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                actualDir = 2;
            }
        }
    }
}
