using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private MonoBehaviour monoBehaviour;
    private PlayerController controller;
    private PlayerPhysics physics;
    private PlayerState currentState;
    private WalkingState walkingState;

    public void Initialize()
    {
        walkingState = new WalkingState(this);
    }
    public void Reference(in MonoBehaviour monoBehaviour, in PlayerController controller, in PlayerPhysics physics) 
    {
        this.monoBehaviour = monoBehaviour;
        this.controller = controller;
        this.physics = physics;
        TransitionToWalking();
    }

    private void ChangeState(in PlayerState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void TransitionToWalking() => ChangeState(walkingState);
    public ref readonly PlayerState CurrentState => ref currentState;
    public ref readonly PlayerController Controller => ref controller;
    public ref readonly PlayerPhysics Physics => ref physics;
    public void EnableUpdate(bool enabled) => monoBehaviour.enabled = enabled;
}
