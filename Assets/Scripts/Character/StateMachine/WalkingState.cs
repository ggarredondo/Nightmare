using UnityEngine;

[System.Serializable]
public class WalkingState : PlayerState
{
    [SerializeField] private float rotationSpeed;
    public void Initialize(in PlayerStateMachine stateMachine) => base.Initialize("WALKING", stateMachine);

    public override void Enter()
    {
        stateMachine.Physics.SwitchToWalkCollider();
        stateMachine.ResetAirJump();
        stateMachine.CollisionHandler.OnTakeOff += stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressJump += Jump;
        base.Enter();
    }

    public override void Update() => stateMachine.EgoHandler.RegenEgo();
    public override void FixedUpdate()
    {
        stateMachine.Physics.RotateRelativeToCamera(stateMachine.Controller.MovementDirection, rotationSpeed);
        stateMachine.Physics.RedirectVelocity();
        stateMachine.Physics.Gravity();
    }

    public override void Exit()
    {
        stateMachine.CollisionHandler.OnTakeOff -= stateMachine.TransitionToFalling;
        stateMachine.Controller.OnPressJump -= Jump;
        base.Exit();
    }

    public void Jump()
    {
        stateMachine.Controller.OnPressJump -= Jump;
        stateMachine.Physics.Jump();
    }
}
