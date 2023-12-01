using System.Collections;
using System;
using UnityEngine;

public class LandingState : PlayerState
{
    public IEnumerator coroutine;
    public LandingState(in PlayerStateMachine stateMachine) : base("LANDING", stateMachine) => coroutine = LandingDelay();

    public override void Enter()
    {
        //stateMachine.StartCoroutine(coroutine);
        stateMachine.TransitionToWalking();
        base.Enter();
    }

    public override void Update() {}
    public override void FixedUpdate() => stateMachine.Physics.Movement(stateMachine.Controller.MovementDirection);

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
