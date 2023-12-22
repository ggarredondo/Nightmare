using UnityEngine;

[System.Serializable]
public class ThrustingState : PlayerState
{
    [SerializeField] private float rotationSpeed;
    public void Initialize(in PlayerStateMachine stateMachine) => base.Initialize("THRUSTING", stateMachine);

    public override void Enter()
    {
        stateMachine.Controller.OnReleaseThrust += stateMachine.TransitionToFalling;
        stateMachine.CollisionHandler.OnLand += stateMachine.TransitionToFalling;
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() 
    { 
        stateMachine.Physics.Thrust();
        stateMachine.Physics.MatchCameraRotation(rotationSpeed);
        // or stateMachine.Physics.RotateRelativeToCamera(stateMachine.Controller.MovementDirection, rotationSpeed);
    }


    public override void Exit()
    {
        stateMachine.Controller.OnReleaseThrust -= stateMachine.TransitionToFalling;
        stateMachine.CollisionHandler.OnLand -= stateMachine.TransitionToFalling;
        base.Exit();
    }
}
