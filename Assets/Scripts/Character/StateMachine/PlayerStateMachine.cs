using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private PlayerController controller;
    private PlayerPhysics physics;
    private CollisionHandler collisionHandler;

    [SerializeField] [ReadOnlyField] private string currentStateName;

    private PlayerState currentState;
    private WalkingState walkingState;
    private FallingState fallingState;
    private LevitationState levitationState;
    private ThrustingState thrustingState;

    public void Initialize(in CollisionHandler collisionHandler)
    {
        this.collisionHandler = collisionHandler;
        walkingState = new WalkingState(this);
        fallingState = new FallingState(this);
        levitationState = new LevitationState(this);
        thrustingState = new ThrustingState(this);
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
        currentStateName = currentState.StateName;
        currentState.Enter();
    }

    public void TransitionToWalking() => ChangeState(walkingState);
    public void TransitionToFalling() => ChangeState(fallingState);
    public void TransitionToLevitation() => ChangeState(levitationState);
    public void TransitionToThrusting() => ChangeState(thrustingState);

    private bool enableAirJump = true;
    public void AirJump()
    {
        if (enableAirJump) {
            physics.AirJump();
            enableAirJump = false;
        }
    }
    public void ResetAirJump() => enableAirJump = true;

    public ref readonly PlayerController Controller => ref controller;
    public ref readonly PlayerPhysics Physics => ref physics;
    public ref readonly CollisionHandler CollisionHandler => ref collisionHandler;

    public ref readonly PlayerState CurrentState => ref currentState;
    public ref readonly WalkingState WalkingState => ref walkingState;
    public ref readonly FallingState FallingState => ref fallingState;
    public ref readonly LevitationState LevitationState => ref levitationState;
    public ref readonly ThrustingState ThrustingState => ref thrustingState;
}
