using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Player");
    public static int SizeID = Shader.PropertyToID("_Size");
    [SerializeField] private Material _mat;
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _size;


 

    void Update()
    {
        var dir = _cam.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        Debug.DrawRay(transform.position, dir,Color.red);

        if (Physics.Raycast(ray, 300000, _mask))
        { 
            _mat.SetFloat(SizeID, _size);
            Debug.Log("Me Here");
        }
        else
        {
            _mat.SetFloat(SizeID, 0);
            Debug.Log("Los Odio");
        }
           

        var view = _cam.WorldToViewportPoint(transform.position);
        _mat.SetVector(PosID, view);
    }
}
