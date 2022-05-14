using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationShields : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private bool isRotate = true;
    [SerializeField] private List<GameObject> _shields;

    void Update()
    {
        if (isRotate)
            transform.Rotate(new Vector3(0, 1 * _rotationSpeed, 0));
    }

    public void ChangeRotateState()
    {
        isRotate = !isRotate;
    }

    public void RestartShields()
    {
        foreach (var s in _shields)
        {
            s.gameObject.SetActive(true);
        }
    }
}
