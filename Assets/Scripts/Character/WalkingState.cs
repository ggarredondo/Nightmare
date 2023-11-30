
public class WalkingState : PlayerState
{
    public WalkingState(in PlayerStateMachine stateMachine) : base("WALKING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.EnableUpdate(true);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() 
    {
        stateMachine.Physics.Movement(stateMachine.Controller.MovementDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
