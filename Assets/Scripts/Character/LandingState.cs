using System.Collections;
using System;
using UnityEngine;

public class LandingState : PlayerState
{
    public IEnumerator coroutine;
    public LandingState(in PlayerStateMachine stateMachine) : base("LANDING", stateMachine) {}

    public override void Enter()
    {
        coroutine = LandingDelay();
        stateMachine.StartCoroutine(coroutine);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() => stateMachine.Physics.WalkingMovement(stateMachine.Controller.MovementDirection);

    public override void Exit()
    {
        stateMachine.StopCoroutine(coroutine);
        base.Exit();
    }

    public IEnumerator LandingDelay()
    {
        yield return new WaitForSeconds((float)TimeSpan.FromMilliseconds(stateMachine.LandingTimeMS).TotalSeconds);
        stateMachine.TransitionToWalking();
    }
}
