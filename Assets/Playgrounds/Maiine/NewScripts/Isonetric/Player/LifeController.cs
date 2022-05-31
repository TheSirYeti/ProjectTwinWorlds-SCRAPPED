using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController
{
    int life = 3;
    bool _isDemon;

    public LifeController(bool isDemon)
    {
        _isDemon = isDemon;
    }

    public void TakeDamage()
    {
        life--;
        EventManager.Trigger("UITakeDamage", _isDemon);

        if (life <= 0)
            LevelManager.instance.ReloadScene();
    }

    public bool Health()
    {
        life++;
        EventManager.Trigger("UITakeHealth", _isDemon);

        if (life < 3)
            return true;
        else return false;
    }
}
