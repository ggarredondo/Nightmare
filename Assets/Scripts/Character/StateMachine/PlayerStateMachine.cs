using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private MonoBehaviour monoBehaviour;
    private PlayerController controller;
    private PlayerPhysics physics;
    private CollisionHandler collisionHandler;
    private AnimationEventHandler animationEventHandler;

    [SerializeField] [ReadOnlyField] private string stateName;

    private PlayerState currentState;
    private WalkingState walkingState;
    private FallingState fallingState;
    private LevitationState levitationState;

    public void Initialize(in MonoBehaviour monoBehaviour, in CollisionHandler collisionHandler, 
        in AnimationEventHandler animationEventHandler)
    {
        this.monoBehaviour = monoBehaviour;
        this.collisionHandler = collisionHandler;
        this.animationEventHandler = animationEventHandler;
        walkingState = new WalkingState(this);
        fallingState = new FallingState(this);
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
    public void TransitionToLevitation() => ChangeState(levitationState);

    public void StartCoroutine(in IEnumerator coroutine) => monoBehaviour.StartCoroutine(coroutine);
    public void StopCoroutine(in IEnumerator coroutine) => monoBehaviour.StopCoroutine(coroutine);
    public void EnableUpdate(bool enabled) => monoBehaviour.enabled = enabled;

    public ref readonly PlayerController Controller => ref controller;
    public ref readonly PlayerPhysics Physics => ref physics;
    public ref readonly CollisionHandler CollisionHandler => ref collisionHandler;
    public ref readonly AnimationEventHandler AnimationEventHandler => ref animationEventHandler;

    public ref readonly PlayerState CurrentState => ref currentState;
    public ref readonly WalkingState WalkingState => ref walkingState;
    public ref readonly FallingState FallingState => ref fallingState;
    public ref readonly LevitationState LevitationState => ref levitationState;
}
