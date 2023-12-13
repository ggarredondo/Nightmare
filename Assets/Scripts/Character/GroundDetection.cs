using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public System.Action OnLand, OnTakeOff;
    private void OnTriggerEnter(Collider other) => OnLand?.Invoke();
    private void OnTriggerExit(Collider other) => OnTakeOff?.Invoke();
}
