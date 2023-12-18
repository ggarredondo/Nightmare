using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private MonoBehaviour monoBehaviour;
    private PlayerController controller;
    private PlayerPhysics physics;
    [SerializeField] private CollisionHandler collisionHandler;

    [SerializeField] [ReadOnlyField] private string stateName;
    [SerializeField] private double landingTimeMS;

    private PlayerState currentState;
    private WalkingState walkingState;
    private FallingState fallingState;
    private LandingState landingState;
    private LevitationState levitationState;

    public void Initialize(in MonoBehaviour monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        walkingState = new WalkingState(this);
        fallingState = new FallingState(this);
        landingState = new LandingState(this);
        levitationState = new LevitationState(this);
    }
    public void Reference(in PlayerController controller, in PlayerPhysics physics) 
    {
        this.controller = controller;
        this.physics = physics;
    }

    private void ChangeState(in PlayerState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        stateName = currentState.StateName;
        currentState.Enter();
    }

    public void TransitionToWalking() => ChangeState(walkingState);
    public void TransitionToFalling() => ChangeState(fallingState);
    public void TransitionToLanding() => ChangeState(landingState);
    public void TransitionToLevitation() => ChangeState(levitationState);

    public void StartCoroutine(in IEnumerator coroutine) => monoBehaviour.StartCoroutine(coroutine);
    public void StopCoroutine(in IEnumerator coroutine) => monoBehaviour.StopCoroutine(coroutine);
    public void EnableUpdate(bool enabled) => monoBehaviour.enabled = enabled;

    public ref readonly CollisionHandler CollisionHandler => ref collisionHandler;
    public ref readonly PlayerController Controller => ref controller;
    public ref readonly PlayerPhysics Physics => ref physics;
    public double LandingTimeMS => landingTimeMS;

    public ref readonly PlayerState CurrentState => ref currentState;
    public ref readonly WalkingState WalkingState => ref walkingState;
    public ref readonly FallingState FallingState => ref fallingState;
    public ref readonly LandingState LandingState => ref landingState;
    public ref readonly LevitationState LevitationState => ref levitationState;
}
