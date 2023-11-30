
public class JumpingState : PlayerState
{
    public JumpingState(in PlayerStateMachine stateMachine) : base("JUMPING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.EnableUpdate(true);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() 
    {
        stateMachine.Physics.Movement(stateMachine.Controller.MovementDirection);
        if (!stateMachine.Physics.IsGrounded) stateMachine.TransitionToFalling();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
