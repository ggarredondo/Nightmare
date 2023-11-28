using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController controller;
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerAnimation playerAnimation;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        controller.Initialize();
        stateMachine.Initialize();
        playerAnimation.Initialize(GetComponent<Animator>());
    }
    private void Start()
    {
        controller.Reference();
        stateMachine.Reference(controller);
        playerAnimation.Reference(controller);
    }

    private void Update() => stateMachine.CurrentState.Update();
    private void FixedUpdate() => stateMachine.CurrentState.FixedUpdate();
}
