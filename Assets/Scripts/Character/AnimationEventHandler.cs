using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event System.Action OnLandExit;
    private void LandExit() => OnLandExit?.Invoke();
}
