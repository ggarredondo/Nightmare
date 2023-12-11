using UnityEngine;

[System.Serializable]
public class PlayerPhysics
{
    private Transform cameraTransform;
    private Rigidbody rb;
    private Collider col;
    private float smoothMagnitude = 0f;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float magnitudeAcceleration;
    [SerializeField] private float groundRaycastOffset = 0.1f;
    [SerializeField] private float jumpingImpulse;
    [SerializeField] [Range(0f, 1f)] private float jumpCancelMultiplier;
    [SerializeField] private float airMovementSpeed;

    public void Initialize(in Rigidbody rb, in Collider col)
    {
        this.rb = rb;
        this.col = col;
        cameraTransform = Camera.main.transform;
    }
    public void Reference() {}

    public event System.Action<float> OnMovement;
    public event System.Action OnJump;

    // Use all following methods in Fixed Update if necessary

    public void Movement(in Vector2 direction)
    {
        if (direction.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        smoothMagnitude = Mathf.Lerp(smoothMagnitude, direction.magnitude, magnitudeAcceleration * Time.fixedDeltaTime);
        OnMovement?.Invoke(smoothMagnitude);
    }

    public void AirMovement(in Vector2 direction)
    {
        Movement(direction);
        rb.AddForce(rb.transform.forward * smoothMagnitude * airMovementSpeed, ForceMode.VelocityChange);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpingImpulse, ForceMode.Impulse);
        OnJump?.Invoke();
    }

    public void CancelJump()
    {
        if (rb.velocity.y > 0f)
            rb.AddForce(-rb.velocity.y * Vector3.up * jumpCancelMultiplier, ForceMode.VelocityChange);
    }

    public void EnableGravity(bool enabled) => rb.useGravity = enabled;

    public float Velocity => new Vector2(rb.velocity.x, rb.velocity.z).magnitude;

    public bool IsGrounded => Physics.Raycast(col.transform.position + Vector3.up * col.bounds.extents.y, -Vector3.up, col.bounds.extents.y + groundRaycastOffset);
}
