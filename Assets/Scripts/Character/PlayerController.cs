using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementDirection;
    public event System.Action<float> OnPressFly;

    public void Initialize() => movementDirection = Vector2.zero;
    public void Reference() {}

    public void PressMovement(InputAction.CallbackContext context) => movementDirection = context.ReadValue<Vector2>();
    public void PressFly(InputAction.CallbackContext context) { if (context.performed) OnPressFly?.Invoke(movementDirection.magnitude); }

    public ref readonly Vector2 MovementDirection => ref movementDirection;
}
