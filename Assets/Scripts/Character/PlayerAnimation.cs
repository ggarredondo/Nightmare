using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    private Animator animator;

    [SerializeField] private float animationSpeed = 1f;

    public void Initialize(in Animator animator) => this.animator = animator;
    public void Reference(in PlayerStateMachine stateMachine, in PlayerPhysics physics) 
    {
        physics.OnWalkingMovement += (float magnitude) => animator.SetFloat("magnitude", magnitude);
        physics.OnJump += () => { animator.applyRootMotion = false; animator.SetTrigger("jump"); };

        stateMachine.WalkingState.OnEnter += () => animator.SetBool("STATE_WALKING", true);
        stateMachine.WalkingState.OnExit += () => animator.SetBool("STATE_WALKING", false);

        stateMachine.FallingState.OnEnter += () => { 
            animator.applyRootMotion = false;
            animator.ResetTrigger("jump");
            animator.SetBool("STATE_FALLING", true); 
        };
        stateMachine.FallingState.OnExit += () => animator.SetBool("STATE_FALLING", false);

        stateMachine.LandingState.OnEnter += () => { animator.applyRootMotion = true; animator.SetBool("STATE_LANDING", true); };
        stateMachine.LandingState.OnExit += () => animator.SetBool("STATE_LANDING", false);
    }

    public void OnValidate() { if (animator) animator.speed = animationSpeed; }
}
