using UnityEngine;
using UnityEngine.InputSystem;
using RefDelegates;

public class PlayerController : MonoBehaviour
{
    public event ActionIn<Vector2> OnMovement;

    public void Initialize() {}
    public void Reference() {}

    public void Movement(InputAction.CallbackContext context) 
    {
        OnMovement?.Invoke(context.ReadValue<Vector2>());
    }
}
