
public class LevitationState : PlayerState
{
    public LevitationState(in PlayerStateMachine stateMachine) : base("LEVITATION", stateMachine) {}

    public override void Enter()
    {
        stateMachine.GroundDetection.OnLand += stateMachine.TransitionToLanding;
        stateMachine.Controller.OnReleaseFly += stateMachine.TransitionToFalling;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);
        stateMachine.Physics.Gravity();
        stateMachine.Physics.CancelFall();
    }

    public override void Exit()
    {
        stateMachine.GroundDetection.OnLand -= stateMachine.TransitionToLanding;
        stateMachine.Controller.OnReleaseFly -= stateMachine.TransitionToFalling;
        base.Exit();
    }
}
