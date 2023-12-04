using System.Collections;
using System;
using UnityEngine;

public class FallingState : PlayerState
{
    public FallingState(in PlayerStateMachine stateMachine) : base("FALLING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.Controller.OnReleaseFly += CancelJump;
        stateMachine.EnableUpdate(true);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);
        if (stateMachine.Physics.IsGrounded) stateMachine.TransitionToLanding();
    }

    public void CancelJump()
    {
        stateMachine.Controller.OnReleaseFly -= CancelJump;
        stateMachine.Physics.CancelJump();
    }

    public override void Exit()
    {
        stateMachine.Controller.OnReleaseFly -= CancelJump;
        base.Exit();
    }
}
