using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image blackScreen;

    RoomChanger actualRoom;

    delegate void FadeDelegate();
    FadeDelegate actualFade = delegate { };

    private void Start()
    {
        EventManager.Subscribe("ChangeFadeState", ChangeFadeState);
    }

    void Update()
    {
        actualFade();
    }

    void ChangeFadeState(params object[] parameter)
    {
        actualFade = FadeOut;
        actualRoom = (RoomChanger)parameter[0];
    }

    void FadeIn()
    {
        blackScreen.color -= new Color(0, 0, 0, 1) * Time.deltaTime;
        if (blackScreen.color.a <= 0)
        {
            actualFade = delegate { };
            actualRoom = null;
        }
    }

    void FadeOut()
    {
        blackScreen.color += new Color(0, 0, 0, 1) * Time.deltaTime;
        if (blackScreen.color.a >= 1)
        {
            StartCoroutine(CorrutinaForFade());
        }
    }

    IEnumerator CorrutinaForFade()
    {
        if (actualFade != null)
            actualRoom.TriggerPlayerTransform();

        yield return new WaitForSeconds(0.5f);
        actualFade = FadeIn;
    }
}
