
public class WalkingState : PlayerState
{
    public WalkingState(in PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() {}

    public override void Exit()
    {
        base.Exit();
    }
}
