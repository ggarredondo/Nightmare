
public class WalkingState : PlayerState
{
    public WalkingState(in PlayerStateMachine stateMachine) : base("WALKING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.Physics.SwitchToWalkCollider();
        stateMachine.ResetAirJump();
        stateMachine.CollisionHandler.OnTakeOff += stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressJump += Jump;
        base.Enter();
    }

    public override void Update() => stateMachine.Controller.UpdateMagnitude();
    public override void FixedUpdate()
    {
        stateMachine.Physics.RotatePlayerGround(stateMachine.Controller.MovementDirection);
        stateMachine.Physics.RedirectVelocity();
        stateMachine.Physics.Gravity();
    }

    public override void Exit()
    {
        stateMachine.CollisionHandler.OnTakeOff -= stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressJump -= Jump;
        base.Exit();
    }

    public void Jump()
    {
        stateMachine.Controller.OnPressJump -= Jump;
        stateMachine.Physics.Jump();
    }
}
