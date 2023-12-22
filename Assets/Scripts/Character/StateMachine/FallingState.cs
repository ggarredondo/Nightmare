using UnityEngine;

[System.Serializable]
public class FallingState : PlayerState
{
    [SerializeField] private float rotationSpeed;

    public event System.Action OnLand;
    public void Initialize(in PlayerStateMachine stateMachine) => base.Initialize("FALLING", stateMachine);

    public override void Enter()
    {
        stateMachine.Physics.SwitchToAirColliders();
        stateMachine.CollisionHandler.OnLand += Land;
        stateMachine.Controller.OnPressLevitate += stateMachine.TransitionToLevitation;
        stateMachine.Controller.OnPressThrust += stateMachine.TransitionToThrusting;
        stateMachine.Controller.OnReleaseJump += CancelJump;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.RotateRelativeToCamera(stateMachine.Controller.MovementDirection, rotationSpeed);
        stateMachine.Physics.RedirectVelocity();
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
        stateMachine.Controller.OnPressLevitate -= stateMachine.TransitionToLevitation;
        stateMachine.Controller.OnPressThrust -= stateMachine.TransitionToThrusting;
        stateMachine.Controller.OnReleaseJump -= CancelJump;
        base.Exit();
    }

    private void Land()
    {
        stateMachine.TransitionToWalking();
        OnLand?.Invoke();
    }
}
