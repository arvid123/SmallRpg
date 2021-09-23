using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private CharacterMovement movement;

    Vector2 mousePosition;
    private bool isMoving = false;

    // Mouse has been moved to point somewhere else
    public void OnPoint(InputAction.CallbackContext context) {
        mousePosition = context.ReadValue<Vector2>();
    }

    // A Movement command has been issued
    public void OnMove(InputAction.CallbackContext context) {
        isMoving = context.ReadValueAsButton();
    }

    void Update()
    {
        // Movement
        if (isMoving) {
            movement.MoveToPosition(Util.screenToGroundPoint(mousePosition, mainCamera));
        }
    }
}
