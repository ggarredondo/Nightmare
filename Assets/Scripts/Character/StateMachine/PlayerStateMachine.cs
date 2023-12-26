using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private PlayerController controller;
    private PlayerPhysics physics;
    private CollisionHandler collisionHandler;
    private EgoHandler egoHandler;

    [SerializeField] [ReadOnlyField] private string currentStateName;
    
    private PlayerState currentState;
    [SerializeField] private WalkingState walkingState;
    [SerializeField] private FallingState fallingState;
    [SerializeField] private LevitationState levitationState;
    [SerializeField] private ThrustingState thrustingState;

    public void Initialize(in CollisionHandler collisionHandler)
    {
        this.collisionHandler = collisionHandler;
        walkingState.Initialize(this);
        fallingState.Initialize(this);
        levitationState.Initialize(this);
        thrustingState.Initialize(this);
    }
    public void Reference(in PlayerController controller, in PlayerPhysics physics, in EgoHandler egoHandler) 
    {
        this.controller = controller;
        this.physics = physics;
        this.egoHandler = egoHandler;
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
    public void TransitionToLevitation()
    {
        if (egoHandler.CurrentEgo > egoHandler.MinUsableEgo)
            ChangeState(levitationState);
    }
    public void TransitionToThrusting()
    {
        if (egoHandler.CurrentEgo > egoHandler.MinUsableEgo)
            ChangeState(thrustingState);
    }

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
    public ref readonly EgoHandler EgoHandler => ref egoHandler;

    public ref readonly PlayerState CurrentState => ref currentState;
    public ref readonly WalkingState WalkingState => ref walkingState;
    public ref readonly FallingState FallingState => ref fallingState;
    public ref readonly LevitationState LevitationState => ref levitationState;
    public ref readonly ThrustingState ThrustingState => ref thrustingState;
}
