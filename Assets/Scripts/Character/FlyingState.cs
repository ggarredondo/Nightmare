
public class FlyingState : PlayerState
{
    public FlyingState(in PlayerStateMachine stateMachine) : base("FLYING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.EnableUpdate(true);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() 
    {
        if (stateMachine.Physics.IsGrounded) stateMachine.TransitionToWalking();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
