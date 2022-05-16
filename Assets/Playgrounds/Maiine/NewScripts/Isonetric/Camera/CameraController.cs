using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform demonTransform;
    public Transform angelTransform;
    public Transform actualTransform;

    Vector3 midCamera;
    Vector3 pointToView;

    bool seeDemon = true;

    public float speed;
    public float rotationAngle;
    public float cameraDist;
    public float maxDistX;
    public float maxDistZ;

    public GameObject blackScreen;

    delegate void CameraDelegate();
    CameraDelegate actualMovement;
    CameraDelegate fadeDelegate;

    void Start()
    {
        EventManager.Subscribe("SeeObject", SeeObject);
        EventManager.Subscribe("ChangePlayer", ChangePlayer);

        transform.parent = null;
        GameObject myMain = Camera.main.gameObject;
        myMain.transform.parent = transform;
        transform.rotation = Quaternion.Euler(new Vector3(rotationAngle, 0, 0));
        myMain.transform.localPosition = new Vector3(0, 0, cameraDist * -1);

        actualTransform = demonTransform;
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
            actualTransform = angelTransform;
        }
        else
        {
            seeDemon = !seeDemon;
            actualTransform = demonTransform;
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

            Debug.Log(distanceX + " X");
            Debug.Log(distanceZ + " Z");

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
    #endregion

    //Corrutina para esperar el tiempo de la vision del objeto
    IEnumerator SeeObjectoCorroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        actualMovement = FollowPlayer;
    }
}
