
using System.Collections;
using System.Collections.Generic;

public interface IState 
{
    void OnStart();
    void OnUpdate();
    void OnExit();
}

public class NullState : IState
{
    public void OnExit()
    {
    }

    public void OnStart()
    {
    }

    public void OnUpdate()
    {
    }
}
