using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    private Animator animator;

    [SerializeField] private float animationSpeed = 1f;

    public void Initialize(in Animator animator) => this.animator = animator;
    public void Reference(in PlayerPhysics physics) 
    {
        physics.OnMovement += (float magnitude) => animator.SetFloat("speed", magnitude);
    }

    public void OnValidate() { if (animator) animator.speed = animationSpeed; }
}
