using UnityEngine;

public class TeleportCollider : MonoBehaviour
{
    [SerializeField] private Vector3 positionToTeleport;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.position = positionToTeleport;
    }
}
