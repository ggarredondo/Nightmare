using System;

public abstract class PlayerState 
{
    protected string stateName;
    protected readonly PlayerStateMachine stateMachine;
    public event Action OnEnter, OnExit;

    public PlayerState(in string stateName, in PlayerStateMachine stateMachine)
    {
        this.stateName = stateName;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() => OnEnter?.Invoke();
    public abstract void Update();
    public abstract void FixedUpdate();
    public virtual void Exit() => OnExit?.Invoke();

    public ref readonly string StateName => ref stateName;
}
