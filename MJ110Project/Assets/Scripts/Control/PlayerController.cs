using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;

    #region MonoBehaviour Methods
    private void Start()
    {
        LockCursor();
    }
    #endregion

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
