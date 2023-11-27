
[System.Serializable]
public class PlayerStateMachine
{
    private PlayerState currentState;
    private WalkingState walkingState;

    public void Initialize() 
    {
        walkingState = new WalkingState();
    }
    public void Reference() 
    {
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
}
