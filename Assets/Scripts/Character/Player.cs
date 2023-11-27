using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine.Initialize();
    }
    private void Start()
    {
        stateMachine.Reference();
    }

    private void Update() => stateMachine.CurrentState.Update();
    private void FixedUpdate() => stateMachine.CurrentState.FixedUpdate();
}
