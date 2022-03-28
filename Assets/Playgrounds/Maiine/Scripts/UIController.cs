using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Image> UI;
    [SerializeField] private bool UIOn;
    private Color c;

    private void Start()
    {
        c.a = 1;
    }

    void Update()
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
