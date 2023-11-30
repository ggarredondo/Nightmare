
public class WalkingState : PlayerState
{
    public WalkingState(in PlayerStateMachine stateMachine) : base("WALKING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.EnableUpdate(true);
        stateMachine.Controller.OnPressFly += stateMachine.Physics.Jump;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() 
    {
        stateMachine.Physics.Movement(stateMachine.Controller.MovementDirection);
        if (!stateMachine.Physics.IsGrounded) stateMachine.TransitionToFalling();
    }

    public override void Exit()
    {
        stateMachine.Controller.OnPressFly -= stateMachine.Physics.Jump;
        base.Exit();
    }
}
