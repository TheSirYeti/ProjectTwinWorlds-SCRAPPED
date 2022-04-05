using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Image> UI;
    [SerializeField] private bool UIOn;

    [SerializeField] private List<GameObject> _demonHearths;
    [SerializeField] private List<GameObject> _angelHearths;

    [SerializeField] private GameObject _demonUI;
    [SerializeField] private GameObject _angelUI;

    [SerializeField] private GameObject _demonPlayer;
    [SerializeField] private GameObject _angelPlayer;

    private bool _isDemond = true;
    private int _uiDemondLife = 3;
    private int _uiAngelLife = 3;
    private Color c;

    private void Start()
    {
        c.a = 1;

        EventManager.Subscribe("OnPlayerHPUpdate", TakeUIDMG);
        EventManager.Subscribe("OnPlayerChange", ChangeUI);
    }

    private void Update()
    {
        if (_isDemond)
        {
            _demonUI.transform.position = _demonPlayer.transform.position;
        }
        else
        {
            _angelUI.transform.position = _angelPlayer.transform.position;
        }
    }

    public void ChangeUI(object[] parameters)
    {
        _isDemond = !_isDemond;
        if (_isDemond)
        {
            _demonUI.gameObject.SetActive(true);
            _angelUI.gameObject.SetActive(false);
        }
        else
        {
            _demonUI.gameObject.SetActive(false);
            _angelUI.gameObject.SetActive(true);
        }
    }

    public void TakeUIDMG(object[] parameters)
    {
        if((bool)parameters[0])
        {
            _uiDemondLife--;
            _demonHearths[_uiDemondLife].gameObject.SetActive(false);
        }
        else
        {
            _uiAngelLife--;
            _angelHearths[_uiAngelLife].gameObject.SetActive(false);
        }
    }

    void FadeOut()
    {
        if (UIOn)
        {
            foreach (var item in UI)
            {
                if (item.color.a <= 1)
                    item.color += c * Time.deltaTime;
            }
        }
        else
        {
            foreach (var item in UI)
            {
                if (item.color.a >= 0)
                    item.color -= c * Time.deltaTime;
            }
        }
    }
}
