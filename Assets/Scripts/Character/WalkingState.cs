
public class WalkingState : PlayerState
{
    public WalkingState(in PlayerStateMachine stateMachine) : base("WALKING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.GroundDetection.OnTakeOff += stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressJump += Jump;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.Movement(stateMachine.Controller.MovementDirection);
        stateMachine.Physics.Gravity();
    }

    public override void Exit()
    {
        stateMachine.GroundDetection.OnTakeOff -= stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressJump -= Jump;
        base.Exit();
    }

    public void Jump()
    {
        stateMachine.Controller.OnPressJump -= Jump;
        stateMachine.Physics.Jump();
    }
}
