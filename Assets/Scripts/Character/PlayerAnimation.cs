using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    private Animator animator;

    public void Initialize(in Animator animator) => this.animator = animator;
    public void Reference(in PlayerController controller)
    {

    }
}
