using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableOnFall : MonoBehaviour
{
    public List<MeshDestroy> myDestroyables;

    public void DestroyObjects()
    {
        SoundManager.instance.PlaySound(SoundID.CRASH);
        foreach (var VARIABLE in myDestroyables)
        {
            VARIABLE.DestroyMesh();
        }
    }
}
