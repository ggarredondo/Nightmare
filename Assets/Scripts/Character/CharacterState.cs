using System;

public abstract class CharacterState 
{
    public event Action OnEnter, OnExit;

    public void Enter() => OnEnter?.Invoke();
    public void Update() {}
    public void FixedUpdate() {}
    public void Exit() => OnExit?.Invoke();
}
