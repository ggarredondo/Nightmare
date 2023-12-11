using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    private Animator animator;
    [SerializeField] private float animationSpeed = 1f;

    public void Initialize(in Animator animator) => this.animator = animator;
    public void Reference(in PlayerController controller, in PlayerStateMachine stateMachine, in PlayerPhysics physics) 
    {
        physics.OnMovement += (float magnitude) => animator.SetFloat("magnitude", magnitude);

        controller.OnPressSprint += () => animator.SetBool("sprinting", true);
        controller.OnReleaseSprint += () => animator.SetBool("sprinting", false);

        PlayerController controllerRef = controller;
        physics.OnJump += () => {
            animator.applyRootMotion = false;
            Vector2 normalized = controllerRef.MovementDirection.normalized;
            animator.SetFloat("horizontal", Mathf.Round(normalized.x));
            animator.SetFloat("vertical", Mathf.Round(normalized.y));
            animator.SetTrigger("jump");
        };

        stateMachine.WalkingState.OnEnter += () => animator.SetBool("STATE_WALKING", true);
        stateMachine.WalkingState.OnExit += () => animator.SetBool("STATE_WALKING", false);

        stateMachine.FallingState.OnEnter += () => { animator.applyRootMotion = false; animator.SetBool("STATE_FALLING", true); };
        stateMachine.FallingState.OnExit += () => animator.SetBool("STATE_FALLING", false);

        PlayerPhysics physicsRef = physics;
        stateMachine.LandingState.OnEnter += () => {
            animator.SetFloat("velocity", physicsRef.Velocity);
            animator.applyRootMotion = true;
            animator.SetBool("STATE_LANDING", true);
        };
        stateMachine.LandingState.OnExit += () => animator.SetBool("STATE_LANDING", false);
    }

    public void OnValidate() { if (animator) animator.speed = animationSpeed; }
}
