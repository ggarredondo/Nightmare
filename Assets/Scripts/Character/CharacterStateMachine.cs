
[System.Serializable]
public class CharacterStateMachine
{
    private CharacterState currentState;

    public void Initialize() {}
    public void Reference() {}

    private void ChangeState(in CharacterState newState) 
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public ref readonly CharacterState CurrentState => ref currentState;
}
