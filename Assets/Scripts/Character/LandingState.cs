using System.Collections;
using System;
using UnityEngine;

public class LandingState : PlayerState
{
    public IEnumerator coroutine;
    public LandingState(in PlayerStateMachine stateMachine) : base("LANDING", stateMachine) {}

    public override void Enter()
    {
        stateMachine.GroundDetection.OnTakeOff += stateMachine.TransitionToFalling;
        coroutine = LandingDelay();
        stateMachine.StartCoroutine(coroutine);
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate()
    {
        stateMachine.Physics.GroundMovement(stateMachine.Controller.MovementDirection);
        stateMachine.Physics.Gravity();
    }

    public override void Exit()
    {
        stateMachine.GroundDetection.OnTakeOff -= stateMachine.TransitionToFalling;
        stateMachine.StopCoroutine(coroutine);
        base.Exit();
    }

    public IEnumerator LandingDelay()
    {
        yield return new WaitForSeconds((float)TimeSpan.FromMilliseconds(stateMachine.LandingTimeMS).TotalSeconds);
        stateMachine.TransitionToWalking();
    }
}
