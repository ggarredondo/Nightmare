using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController controller;
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerPhysics physics;
    [SerializeField] private PlayerAnimation playerAnimation;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        controller.Initialize();
        stateMachine.Initialize();
        physics.Initialize(GetComponent<Rigidbody>());
        playerAnimation.Initialize(GetComponent<Animator>());
    }
    private void Start()
    {
        controller.Reference();
        physics.Reference();
        playerAnimation.Reference(physics);
        stateMachine.Reference(this, controller, physics);
    }
    private void OnValidate()
    {
        playerAnimation.OnValidate();
    }

    private void Update() => stateMachine.CurrentState.Update();
    private void FixedUpdate() => stateMachine.CurrentState.FixedUpdate();
}
