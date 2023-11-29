using UnityEngine;

[System.Serializable]
public class PlayerPhysics
{
    private Transform cameraTransform;
    private Rigidbody rb;
    private Collider col;
    private float smoothMagnitude = 0f;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementAcceleration;

    public void Initialize(in Rigidbody rb, in Collider col)
    {
        this.rb = rb;
        this.col = col;
        cameraTransform = Camera.main.transform;
    }
    public void Reference() {}

    public event System.Action<float> OnMovement;

    /// <summary>
    /// Use in Fixed Update.
    /// </summary>
    public void Movement(in Vector2 direction)
    {
        if (direction.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        smoothMagnitude = Mathf.Lerp(smoothMagnitude, direction.magnitude, movementAcceleration * Time.fixedDeltaTime);
        OnMovement?.Invoke(smoothMagnitude);
    }

    public bool IsGrounded => Physics.Raycast(rb.position, -Vector3.up, 0.1f);
}
