using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    private Animator animator;

    [SerializeField] private float animationSpeed = 1f;

    public void Initialize(in Animator animator) => this.animator = animator;
    public void Reference(in PlayerStateMachine stateMachine, in PlayerPhysics physics) 
    {
        physics.OnMovement += (float magnitude) => animator.SetFloat("magnitude", magnitude);
        physics.OnJump += () => { animator.applyRootMotion = false; animator.SetTrigger("jump"); };
        stateMachine.FallingState.OnEnter += () => { animator.applyRootMotion = false; animator.SetTrigger("fall"); };
        stateMachine.LandingState.OnEnter += () => { animator.applyRootMotion = true; animator.SetTrigger("land"); };
    }

    public void OnValidate() { if (animator) animator.speed = animationSpeed; }
}
