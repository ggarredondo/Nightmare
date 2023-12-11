using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public event System.Action OnLand, OnTakeOff;
    private void OnCollisionEnter(Collision collision) { if (collision.gameObject.tag == "Ground") OnLand?.Invoke(); }
    private void OnCollisionExit(Collision collision) { if (collision.gameObject.tag == "Ground") OnTakeOff?.Invoke(); }
}
