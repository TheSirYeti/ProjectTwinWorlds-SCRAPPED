using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksController : MonoBehaviour
{
    Renderer _myRenderer;

    public Material green;

    private void Start()
    {
        _myRenderer = GetComponent<Renderer>();
    }

    public void ActiveMarks()
    {
        _myRenderer.material = green;
    }
}
