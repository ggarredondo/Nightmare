using System;

public abstract class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    public event Action OnEnter, OnExit;

    public PlayerState(in PlayerStateMachine stateMachine) => this.stateMachine = stateMachine;
    public virtual void Enter() => OnEnter?.Invoke();
    public abstract void Update();
    public abstract void FixedUpdate();
    public virtual void Exit() => OnExit?.Invoke();
}
