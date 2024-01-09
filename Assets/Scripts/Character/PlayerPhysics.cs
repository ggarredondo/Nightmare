using UnityEngine;

[System.Serializable]
public class PlayerPhysics
{
    private Transform cameraTransform;
    private Rigidbody rb;
    [SerializeField] private Collider walkCollider;
    [SerializeField] private Collider[] airColliders;

    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float maxLinearVelocity;
    [SerializeField] private float jumpingImpulse;
    [SerializeField] [Range(0f, 1f)] private float jumpCancelMultiplier;
    [SerializeField] private float airJumpingImpulse;
    [SerializeField] private float thrustAcceleration;
    [SerializeField] private float thrustVerticalOffset;

    public void Initialize(in Rigidbody rb)
    {
        this.rb = rb;
        rb.useGravity = false;
        rb.maxLinearVelocity = this.maxLinearVelocity;
    }
    public void Reference() => cameraTransform = Camera.main.transform;

    public void RotateRelativeToCamera(in Vector2 direction, float rotationSpeed)
    {
        if (direction.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void MatchCameraRotation(float rotationSpeed)
    {
        Quaternion targetRotation = Quaternion.Euler(0f, cameraTransform.rotation.eulerAngles.y, 0f);
        rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    public void RedirectVelocity()
    {
        Vector3 planeVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.velocity = rb.transform.forward * planeVelocity.magnitude + rb.velocity.y * Vector3.up;
    }

    public event System.Action OnJump;
    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpingImpulse, ForceMode.Impulse);
        OnJump?.Invoke();
    }

    public void AirJump() => rb.AddForce(Vector3.up * airJumpingImpulse, ForceMode.Impulse);

    public void CancelJump()
    {
        if (rb.velocity.y > 0f)
            rb.AddForce(-rb.velocity.y * Vector3.up * jumpCancelMultiplier, ForceMode.VelocityChange);
    }

    /// <summary>
    /// This function adds physics' default gravity as acceleration to the rigidbody.
    /// If the player is falling, gravity is scaled for effect.
    /// </summary>
    public void Gravity()
    {
        Vector3 gravity = rb.velocity.y < 0f ? gravityScale * Physics.gravity : Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    public void Levitate()
    {
        if (rb.velocity.y > 0f) Gravity();
        else rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    public void Thrust() 
        => rb.AddForce((cameraTransform.forward + Vector3.up * thrustVerticalOffset).normalized * thrustAcceleration, ForceMode.Acceleration);

    public void SwitchToWalkCollider()
    {
        walkCollider.enabled = true;
        for (int i = 0; i < airColliders.Length; ++i)
            airColliders[i].enabled = false;
    }
    public void SwitchToAirColliders()
    {
        walkCollider.enabled = false;
        for (int i = 0; i < airColliders.Length; ++i)
            airColliders[i].enabled = true;
    }

    public float Velocity => new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
}
