using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController controller;
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerPhysics physics;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private EgoHandler egoHandler;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        controller.Initialize();
        stateMachine.Initialize(GetComponent<CollisionHandler>());
        physics.Initialize(GetComponent<Rigidbody>());
        playerAnimation.Initialize(GetComponent<Animator>());
        egoHandler.Initialize();
    }
    private void Start()
    {
        controller.Reference();
        physics.Reference();
        stateMachine.Reference(controller, physics ,egoHandler);
        playerAnimation.Reference(controller, stateMachine, physics); 
        stateMachine.TransitionToWalking(); // Must be done last
    }
    private void OnValidate()
    {
        playerAnimation.OnValidate();
    }

    private void Update() => stateMachine.CurrentState.Update();
    private void FixedUpdate() => stateMachine.CurrentState.FixedUpdate();

    public ref readonly EgoHandler EgoHandler => ref egoHandler;
}
