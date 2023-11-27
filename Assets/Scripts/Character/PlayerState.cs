using System;

public abstract class PlayerState 
{
    public event Action OnEnter, OnExit;

    public virtual void Enter() => OnEnter?.Invoke();
    public abstract void Update();
    public abstract void FixedUpdate();
    public virtual void Exit() => OnExit?.Invoke();
}
