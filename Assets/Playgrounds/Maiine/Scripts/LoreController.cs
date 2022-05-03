using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoreController : MonoBehaviour
{
    public List<GameObject> loreText;
    public int index = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < loreText.Count - 1)
            {
                loreText[index].gameObject.SetActive(false);
                index++;
                loreText[index].gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("der");
            if(index > 0)
            {
                loreText[index].gameObject.SetActive(false);
                index--;
                loreText[index].gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }
}
