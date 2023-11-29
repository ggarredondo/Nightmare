using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private MonoBehaviour monoBehaviour;
    private PlayerController controller;
    private PlayerPhysics physics;

    private PlayerState currentState;
    private WalkingState walkingState;
    private FlyingState flyingState;

    public void Initialize(in MonoBehaviour monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        walkingState = new WalkingState(this);
        flyingState = new FlyingState(this);
    }
    public void Reference(in PlayerController controller, in PlayerPhysics physics) 
    {
        this.controller = controller;
        this.physics = physics;
        TransitionToWalking();
    }

    private void ChangeState(in PlayerState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void TransitionToWalking() => ChangeState(walkingState);
    public void TransitionToFlying() => ChangeState(flyingState);

    public ref readonly PlayerState CurrentState => ref currentState;
    public ref readonly WalkingState WalkingState => ref walkingState;
    public ref readonly FlyingState FlyingState => ref flyingState;

    public ref readonly PlayerController Controller => ref controller;
    public ref readonly PlayerPhysics Physics => ref physics;
    public void EnableUpdate(bool enabled) => monoBehaviour.enabled = enabled;
}
