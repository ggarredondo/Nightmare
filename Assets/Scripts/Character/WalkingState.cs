
public class WalkingState : PlayerState
{
    public WalkingState(in PlayerStateMachine stateMachine) : base("WALKING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.GroundDetection.OnTakeOff += stateMachine.TransitionToFalling;
        stateMachine.EnableUpdate(true);
        stateMachine.Controller.OnPressFly += Jump;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() => stateMachine.Physics.Movement(stateMachine.Controller.MovementDirection);

    public override void Exit()
    {
        stateMachine.GroundDetection.OnTakeOff -= stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressFly -= Jump;
        base.Exit();
    }

    public void Jump()
    {
        stateMachine.Controller.OnPressFly -= Jump;
        stateMachine.Physics.Jump();
    }
}
