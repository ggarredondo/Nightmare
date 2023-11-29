
public class WalkingState : PlayerState
{
    public WalkingState(in PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.EnableUpdate(true);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() 
    {
        stateMachine.Physics.Movement(stateMachine.Controller.MovementDirection);
        UnityEngine.Debug.Log(stateMachine.Physics.IsGrounded);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
