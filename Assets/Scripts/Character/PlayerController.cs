using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementDirection;
    public event System.Action OnPressFly, OnReleaseFly;
    public event System.Action OnPressSprint, OnReleaseSprint;


    public void Initialize() => movementDirection = Vector2.zero;
    public void Reference() {}

    public void PressMovement(InputAction.CallbackContext context) => movementDirection = context.ReadValue<Vector2>();
    public void PressSprint(InputAction.CallbackContext context)
    {
        if (context.performed) OnPressSprint?.Invoke();
        else if (context.canceled) OnReleaseSprint?.Invoke();
    }
    public void PressFly(InputAction.CallbackContext context)
    {
        if (context.performed) OnPressFly?.Invoke();
        else if (context.canceled) OnReleaseFly?.Invoke();
    }

    public ref readonly Vector2 MovementDirection => ref movementDirection;
}
