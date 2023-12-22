
public class LevitationState : PlayerState
{
    public LevitationState(in PlayerStateMachine stateMachine) : base("LEVITATION", stateMachine) {}

    public override void Enter()
    {
        stateMachine.AirJump();
        stateMachine.Controller.OnReleaseLevitate += stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressThrust += stateMachine.TransitionToThrusting;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);
        stateMachine.Physics.Levitate();
    }

    public override void Exit()
    {
        stateMachine.Controller.OnReleaseLevitate -= stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressThrust -= stateMachine.TransitionToThrusting;
        base.Exit();
    }
}
