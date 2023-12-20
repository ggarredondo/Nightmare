using UnityEngine;

public class RootMotionEnabler : StateMachineBehaviour
{
    [SerializeField] private bool enable = true;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.applyRootMotion = enable;
}
