
public class FallingState : PlayerState
{
    public FallingState(in PlayerStateMachine stateMachine) : base("FALLING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.EnableUpdate(true);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);
        if (stateMachine.Physics.IsGrounded) stateMachine.TransitionToLanding();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
