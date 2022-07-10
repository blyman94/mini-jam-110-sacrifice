using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerInput playerInput;

    #region MonoBehaviour Methods
    private void Start()
    {
        UnlockCursor();
        playerInput.DeactivateInput();
    }
    #endregion

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            player.Interactor.Interact();
        }
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.Mover3D.MoveInput = context.ReadValue<Vector2>();
        }
        else
        {
            player.Mover3D.MoveInput = Vector2.zero;
        }
    }
}
