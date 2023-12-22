using UnityEngine;

[System.Serializable]
public class LevitationState : PlayerState
{
    [SerializeField] private float rotationSpeed;
    public void Initialize(in PlayerStateMachine stateMachine) => base.Initialize("LEVITATION", stateMachine);

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
        stateMachine.Physics.RotateRelativeToCamera(stateMachine.Controller.MovementDirection, rotationSpeed);
        stateMachine.Physics.RedirectVelocity();
        stateMachine.Physics.Levitate();
    }

    public override void Exit()
    {
        stateMachine.Controller.OnReleaseLevitate -= stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressThrust -= stateMachine.TransitionToThrusting;
        base.Exit();
    }
}
