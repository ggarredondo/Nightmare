
public class FallingState : PlayerState
{
    public FallingState(in PlayerStateMachine stateMachine) : base("FALLING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.Physics.SwitchToAirColliders();
        stateMachine.GroundDetection.OnLand += stateMachine.TransitionToLanding;
        stateMachine.Controller.OnReleaseFly += CancelJump;
        stateMachine.EnableUpdate(true);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() => stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);

    public void CancelJump()
    {
        stateMachine.Controller.OnReleaseFly -= CancelJump;
        stateMachine.Physics.CancelJump();
    }

    public override void Exit()
    {
        stateMachine.Physics.SwitchToWalkCollider();
        stateMachine.GroundDetection.OnLand -= stateMachine.TransitionToLanding;
        stateMachine.Controller.OnReleaseFly -= CancelJump;
        base.Exit();
    }
}
