using System;
using System.Collections;
using UnityEngine;

public class WalkingState : PlayerState
{
    private IEnumerator coroutine;
    public WalkingState(in PlayerStateMachine stateMachine) : base("WALKING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.EnableUpdate(true);
        coroutine = CoyoteDelay();
        stateMachine.Controller.OnPressFly += Jump;
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
        stateMachine.StopCoroutine(coroutine);
        stateMachine.Controller.OnPressFly -= Jump;
        base.Exit();
    }

    public void Jump(float magnitude)
    {
        stateMachine.Controller.OnPressFly -= Jump;
        stateMachine.Physics.Jump(magnitude);
    }

    public IEnumerator CoyoteDelay()
    {
        stateMachine.Physics.EnableGravity(false);
        yield return new WaitForSeconds((float)TimeSpan.FromMilliseconds(stateMachine.CoyoteTimeMS).TotalSeconds);
        stateMachine.Physics.EnableGravity(true);
        stateMachine.TransitionToFalling();
    }
}
