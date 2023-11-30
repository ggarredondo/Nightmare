using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private MonoBehaviour monoBehaviour;
    private PlayerController controller;
    private PlayerPhysics physics;

    [SerializeField] [ReadOnlyField] private string stateName;
    private PlayerState currentState;
    private WalkingState walkingState;
    private JumpingState jumpingState;
    private FallingState fallingState;

    public void Initialize(in MonoBehaviour monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        walkingState = new WalkingState(this);
        jumpingState = new JumpingState(this);
        fallingState = new FallingState(this);
    }
    public void Reference(in PlayerController controller, in PlayerPhysics physics) 
    {
        this.controller = controller;
        this.physics = physics;
    }

    private void ChangeState(in PlayerState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        stateName = currentState.StateName;
        currentState.Enter();
    }

    public void TransitionToWalking() => ChangeState(walkingState);
    public void TransitionToJumping() => ChangeState(jumpingState);
    public void TransitionToFalling() => ChangeState(fallingState);

    public ref readonly PlayerState CurrentState => ref currentState;
    public ref readonly WalkingState WalkingState => ref walkingState;
    public ref readonly JumpingState JumpingState => ref jumpingState;
    public ref readonly FallingState FallingState => ref fallingState;

    public ref readonly PlayerController Controller => ref controller;
    public ref readonly PlayerPhysics Physics => ref physics;
    public void EnableUpdate(bool enabled) => monoBehaviour.enabled = enabled;
}
