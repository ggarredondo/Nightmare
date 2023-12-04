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
        stateMachine.Initialize(this);
        physics.Initialize(GetComponent<Rigidbody>(), GetComponent<Collider>());
        playerAnimation.Initialize(GetComponent<Animator>());
    }
    private void Start()
    {
        controller.Reference();
        physics.Reference();
        stateMachine.Reference(controller, physics);
        playerAnimation.Reference(controller, stateMachine, physics); 
        stateMachine.TransitionToWalking(); // Must be done last
    }
    private void OnValidate()
    {
        playerAnimation.OnValidate();
    }

    private void Update() => stateMachine.CurrentState.Update();
    private void FixedUpdate() => stateMachine.CurrentState.FixedUpdate();
}
