using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player demon;
    public Player angel;
    public Transform actualTransform;

    public Transform rotationAxiesX;

    public Vector3 midCamera;
    Vector3 pointToView;

    bool seeDemon = true;

    bool rotateRight = true;

    public float speed;
    public float rotationSpeed;
    public float rotationAngle;
    public float rotationAngleY;
    public float cameraDist;
    public float maxDistX;
    public float maxDistZ;

    public float rotateToPoint;

    delegate void CameraDelegate();
    CameraDelegate actualMovement;

    void Start()
    {
        EventManager.Subscribe("SeeObject", SeeObject);
        EventManager.Subscribe("ChangePlayer", ChangePlayer);

        transform.parent = null;
        GameObject myMain = Camera.main.gameObject;
        myMain.transform.parent = rotationAxiesX;
        transform.rotation = Quaternion.Euler(new Vector3(0, rotationAngleY, 0));
        rotationAxiesX.localRotation = Quaternion.Euler(new Vector3(rotationAngle, 0, 0));
        Camera.main.orthographicSize = cameraDist;

        actualTransform = demon.transform;
        myMain.transform.LookAt(actualTransform);
        actualMovement = FollowPlayer;
    }

    private void Update()
    {
        actualMovement();
    }

    //Receptor de Botones
    public void ChangeAimState(bool isDown)
    {
        if (isDown)
            SetAim(true);
        else if (!isDown)
            SetAim(false);
    }

    #region Cambios del Delegate de Movimiento
    //Cambia el personaje al que tiene que seguir
    public void ChangePlayer(params object[] parameter)
    {
        if (seeDemon)
        {
            seeDemon = !seeDemon;
            actualTransform = angel.transform;
        }
        else
        {
            seeDemon = !seeDemon;
            actualTransform = demon.transform;
        }
    }

    //Activa o desactiva el modo de aim
    public void SetAim(bool isAiming)
    {
        if (isAiming)
            actualMovement = Aim;
        else
            actualMovement = FollowPlayer;
    }

    //Mira a un objeto especifico, x un tiempo especifico
    public void SeeObject(params object[] parameter)
    {
        pointToView = (Vector3)parameter[0];
        actualMovement = SeePoint;
        StartCoroutine(SeeObjectoCorroutine((float)parameter[1]));
    }


    public void SetRotate(bool _rotateRight, float _pointToRotate)
    {
        rotateRight = _rotateRight;
        rotateToPoint = _pointToRotate;
        ChangeControllers(false);
        actualMovement += RotateCamera;
    }

    public void ChangeControllers(bool isActivate)
    {
        if (isActivate)
        {
            if (seeDemon)
                demon.SetOnDelegates();
            else
                angel.SetOnDelegates();
        }
        else
        {
            if (seeDemon)
                demon.SetOffDelegates();
            else
                angel.SetOffDelegates();
        }
    }

    #endregion

    #region Posibles Delegate Movimiento
    //Sigue al player seleccionado
    void FollowPlayer()
    {
        transform.position += (actualTransform.position - transform.position) * speed * Time.deltaTime;
    }

    //Sigue al punto de aim con limites
    void Aim()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            Vector3 rayPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 dist = (rayPoint - actualTransform.position) * 0.5f;
            midCamera = actualTransform.position + dist;

            float distanceX = actualTransform.position.x - midCamera.x;
            float distanceZ = actualTransform.position.z - midCamera.z;

            distanceX = Mathf.Abs(distanceX);
            distanceZ = Mathf.Abs(distanceZ);

            if (distanceX > maxDistX)
            {
                if (midCamera.x > actualTransform.position.x)
                {
                    midCamera.x = actualTransform.position.x + maxDistX;
                }
                else if (midCamera.x < actualTransform.position.x)
                {
                    midCamera.x = actualTransform.position.x - maxDistX;
                }
            }

            if (distanceZ > maxDistZ)
            {
                if (midCamera.z > actualTransform.position.z)
                {
                    midCamera.z = actualTransform.position.z + maxDistZ;
                }
                else if (midCamera.z < actualTransform.position.z)
                {
                    midCamera.z = actualTransform.position.z - maxDistZ;
                }
            }

            transform.position += (midCamera - transform.position) * speed * Time.deltaTime;
        }
    }

    //Mira un punto exacto que se le haya pasado
    void SeePoint()
    {
        transform.position += (pointToView - transform.position) * speed * Time.deltaTime;
    }

    //rota al rededor del eje Y
    void RotateCamera()
    {
        if (rotateRight)
        {
            transform.Rotate(new Vector3(0, rotationSpeed, 0));

            Vector3 actualRotate = transform.rotation.eulerAngles;

            if (actualRotate.y >= rotateToPoint)
            {
                actualMovement -= RotateCamera;
                ChangeControllers(true);
            }
        }
        else
        {
            transform.Rotate(new Vector3(0, -rotationSpeed, 0));

            Vector3 actualRotate = transform.rotation.eulerAngles;

            if (actualRotate.y <= rotateToPoint)
            {
                actualMovement -= RotateCamera;
                ChangeControllers(true);
            }
        }
    }
    #endregion

    //Corrutina para esperar el tiempo de la vision del objeto
    IEnumerator SeeObjectoCorroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        actualMovement = FollowPlayer;
    }
}
