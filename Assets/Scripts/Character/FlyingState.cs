
public class FlyingState : PlayerState
{
    public FlyingState(in PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.EnableUpdate(false);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() {}

    public override void Exit()
    {
        base.Exit();
    }
}
