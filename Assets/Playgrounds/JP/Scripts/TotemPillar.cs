using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TotemPillar : MonoBehaviour
{
    private bool isActive = false;
    public List<GameObject> objectsToChangeMat;
    public List<Material> materials;
    
    public void EnableTotem()
    {
        isActive = true;

        Debug.Log("DOUUUUUUUUUUUU");
        
        for (int i = 0; i < objectsToChangeMat.Count; i++)
        {
            objectsToChangeMat[i].GetComponent<Renderer>().material = materials[i];
        }
    }
}
