using UnityEngine;

[System.Serializable]
public class ThrustingState : PlayerState
{
    [SerializeField] private float rotationSpeed, depletionRate;
    public void Initialize(in PlayerStateMachine stateMachine) => base.Initialize("THRUSTING", stateMachine);

    public override void Enter()
    {
        stateMachine.Controller.OnReleaseThrust += stateMachine.TransitionToFalling;
        stateMachine.CollisionHandler.OnLand += stateMachine.TransitionToFalling;
        stateMachine.EgoHandler.OnEgoDepletion += stateMachine.TransitionToFalling;
        base.Enter();
    }

    public override void Update() => stateMachine.EgoHandler.DepleteEgo(depletionRate);
    public override void FixedUpdate() 
    { 
        stateMachine.Physics.Thrust();
        stateMachine.Physics.MatchCameraRotation(rotationSpeed);
    }


    public override void Exit()
    {
        stateMachine.Controller.OnReleaseThrust -= stateMachine.TransitionToFalling;
        stateMachine.CollisionHandler.OnLand -= stateMachine.TransitionToFalling;
        stateMachine.EgoHandler.OnEgoDepletion -= stateMachine.TransitionToFalling;
        base.Exit();
    }
}
