
public class LandingState : PlayerState
{
    public LandingState(in PlayerStateMachine stateMachine) : base("LANDING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.CollisionHandler.OnTakeOff += stateMachine.TransitionToFalling;
        stateMachine.AnimationEventHandler.OnLandExit += stateMachine.TransitionToWalking;
        stateMachine.Controller.OnPressJump += Jump;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);
        stateMachine.Physics.Gravity();
    }

    public override void Exit()
    {
        stateMachine.CollisionHandler.OnTakeOff -= stateMachine.TransitionToFalling;
        stateMachine.AnimationEventHandler.OnLandExit -= stateMachine.TransitionToWalking;
        stateMachine.Controller.OnPressJump -= Jump;
        base.Exit();
    }

    public void Jump()
    {
        stateMachine.Controller.OnPressJump -= Jump;
        stateMachine.Physics.Jump();
    }
}
