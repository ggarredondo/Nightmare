
public class FallingState : PlayerState
{
    public event System.Action OnLand;
    public FallingState(in PlayerStateMachine stateMachine) : base("FALLING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.Physics.SwitchToAirColliders();
        stateMachine.CollisionHandler.OnLand += Land;
        stateMachine.Controller.OnPressFly += stateMachine.TransitionToLevitation;
        stateMachine.Controller.OnReleaseJump += CancelJump;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);
        stateMachine.Physics.Gravity();
    }

    public void CancelJump()
    {
        stateMachine.Controller.OnReleaseJump -= CancelJump;
        stateMachine.Physics.CancelJump();
    }

    public override void Exit()
    {
        stateMachine.CollisionHandler.OnLand -= Land;
        stateMachine.Controller.OnPressFly -= stateMachine.TransitionToLevitation;
        stateMachine.Controller.OnReleaseJump -= CancelJump;
        base.Exit();
    }

    private void Land()
    {
        stateMachine.TransitionToWalking();
        OnLand?.Invoke();
    }
}
