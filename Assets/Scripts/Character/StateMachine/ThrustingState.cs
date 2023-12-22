
public class ThrustingState : PlayerState
{
    public ThrustingState(in PlayerStateMachine stateMachine) : base("THRUSTING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.Controller.OnReleaseThrust += stateMachine.TransitionToFalling;
        stateMachine.CollisionHandler.OnLand += stateMachine.TransitionToFalling;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() => stateMachine.Physics.Thrust();

    public override void Exit()
    {
        stateMachine.Controller.OnReleaseThrust -= stateMachine.TransitionToFalling;
        stateMachine.CollisionHandler.OnLand -= stateMachine.TransitionToFalling;
        base.Exit();
    }
}
