using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] GameObject _darkVolume;
    [SerializeField] GameObject _simpleVolume;
    [SerializeField] bool _simpleVolumeBool;

    // Update is called once per frame
    private void Start()
    {
        _simpleVolumeBool = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_simpleVolumeBool == true)
            {
                _darkVolume.gameObject.SetActive(true);
                _simpleVolumeBool = false;
            }
            else
            {
                _darkVolume.gameObject.SetActive(false);
                _simpleVolumeBool = true;
            }
        }
    }
}
