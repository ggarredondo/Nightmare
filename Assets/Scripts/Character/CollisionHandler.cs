using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private GameObject lastGround;
    public event System.Action OnLand, OnTakeOff;

    private void OnCollisionEnter(Collision collision)
    {
        if (Vector3.Angle(Vector3.up, collision.GetContact(0).normal) == 0)
        {
            lastGround = collision.gameObject;
            OnLand?.Invoke();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (lastGround == collision.gameObject)
            OnTakeOff?.Invoke();
    }
}
