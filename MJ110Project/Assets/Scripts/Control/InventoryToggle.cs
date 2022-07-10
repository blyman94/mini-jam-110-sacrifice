using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    private bool inventoryOpen = false;

    [SerializeField] private CanvasGroupRevealer inventoryCanvasGroupRevealer;
    [SerializeField] private PlayerInput playerInput;

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryOpen)
            {
                LockCursor();
                playerInput.ActivateInput();
                inventoryCanvasGroupRevealer.HideGroup();
                inventoryOpen = false;
            }
            else
            {
                UnlockCursor();
                playerInput.DeactivateInput();
                inventoryCanvasGroupRevealer.ShowGroup();
                inventoryOpen = true;
            }
        }
    }
}
