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

    public LayerMask collisionRaycast;

    bool seeDemon = true;

    public float speed;
    public float rotationSpeed;
    public float rotationAngle;
    public float rotationAngleY;
    public float cameraDist;
    public float maxDistX;
    public float maxDistZ;

    public float distanceToAim;

    public float nearClip;
    public float farClip;

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
        myMain.transform.rotation = Quaternion.Euler(Vector3.zero);
        myMain.transform.localPosition = Vector3.zero;

        transform.rotation = Quaternion.Euler(new Vector3(0, rotationAngleY, 0));

        rotationAxiesX.rotation = Quaternion.Euler(Vector3.zero);
        rotationAxiesX.localRotation = Quaternion.Euler(new Vector3(rotationAngle, 0, 0));

        Camera.main.orthographicSize = cameraDist;
        Camera.main.nearClipPlane = nearClip;
        Camera.main.farClipPlane = farClip;

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
        /*pointToView = (Vector3)parameter[0];
        actualMovement = SeePoint;*/
        actualMovement = delegate { };
        transform.position = (Vector3)parameter[0];
        StartCoroutine(SeeObjectoCorroutine((float)parameter[1]));
    }


    public void SetRotate(bool _rotateRight, float _pointToRotate)
    {
        rotateToPoint = _pointToRotate;
        ChangeControllers(false);
        actualMovement = RotateCamera;
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

    void Aim()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, collisionRaycast))
        {
            Vector3 rayPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 dir = (rayPoint - actualTransform.position).normalized;
            midCamera = actualTransform.position + dir * distanceToAim;


            midCamera.y = actualTransform.position.y;

            float midDist = Vector3.Distance(actualTransform.position, midCamera);
            float rayDist = Vector3.Distance(actualTransform.position, rayPoint);

            if (midDist <= rayDist)
                transform.position += (midCamera - transform.position) * speed * Time.deltaTime;
            else
                transform.position += (actualTransform.position - transform.position) * speed * Time.deltaTime;
        }
    }

    //rota al rededor del eje Y
    void RotateCamera()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));

        Vector3 actualRotate = transform.rotation.eulerAngles;
        Debug.Log(transform.rotation.eulerAngles);
        Debug.Log(rotateToPoint);

        if (actualRotate.y >= rotateToPoint)
        {
            actualMovement = FollowPlayer;
            ChangeControllers(true);
        }
    }
    #endregion

    //Corrutina para esperar el tiempo de la vision del objeto
    IEnumerator SeeObjectoCorroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = actualTransform.position;
        actualMovement = FollowPlayer;
    }
}
