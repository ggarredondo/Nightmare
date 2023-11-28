
[System.Serializable]
public class PlayerStateMachine
{
    private PlayerController controller;
    private PlayerState currentState;
    private WalkingState walkingState;

    public void Initialize()
    {
        walkingState = new WalkingState(this);
    }
    public void Reference(in PlayerController controller) 
    {
        this.controller = controller;
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
}
