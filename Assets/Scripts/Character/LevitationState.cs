
public class LevitationState : PlayerState
{
    public LevitationState(in PlayerStateMachine stateMachine) : base("LEVITATION", stateMachine) {}

    public override void Enter()
    {
        stateMachine.Physics.EnableGravity(false);
        stateMachine.Controller.OnReleaseFly += stateMachine.TransitionToFalling;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);
        stateMachine.Physics.CancelFall();
    }

    public override void Exit()
    {
        stateMachine.Physics.EnableGravity(true);
        stateMachine.Controller.OnReleaseFly -= stateMachine.TransitionToFalling;
        base.Exit();
    }
}
