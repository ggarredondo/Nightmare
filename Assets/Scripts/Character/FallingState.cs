using System.Collections;
using System;
using UnityEngine;

public class FallingState : PlayerState
{
    private bool keepJumping;
    private IEnumerator coroutine;

    public FallingState(in PlayerStateMachine stateMachine) : base("FALLING", stateMachine) {}

    public override void Enter()
    {
        keepJumping = true;
        coroutine = CancelJumpByTime();
        stateMachine.StartCoroutine(coroutine);
        stateMachine.Controller.OnReleaseFly += CancelJumpByInput;

        stateMachine.EnableUpdate(true);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.AirMovement(stateMachine.Controller.MovementDirection);
        if (stateMachine.Physics.IsGrounded) stateMachine.TransitionToLanding();
        if (keepJumping) stateMachine.Physics.Jump();
    }

    public override void Exit()
    {
        stateMachine.Controller.OnReleaseFly -= CancelJumpByInput;
        base.Exit();
    }

    public void CancelJumpByInput() => keepJumping = false;
    public IEnumerator CancelJumpByTime()
    {
        yield return new WaitForSeconds((float)TimeSpan.FromMilliseconds(stateMachine.JumpMaxTimeMS).TotalSeconds);
        keepJumping = false;
    }
}
