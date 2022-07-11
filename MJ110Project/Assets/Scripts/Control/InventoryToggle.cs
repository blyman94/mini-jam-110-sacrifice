using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    public bool InventoryOpen = false;

    [SerializeField] private MenuModelRotator menuModelRotator;
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
            if (!menuModelRotator.IsInMenu)
            {
                if (InventoryOpen)
                {
                    LockCursor();
                    playerInput.ActivateInput();
                    inventoryCanvasGroupRevealer.HideGroup();
                    InventoryOpen = false;
                }
                else
                {
                    UnlockCursor();
                    playerInput.DeactivateInput();
                    inventoryCanvasGroupRevealer.ShowGroup();
                    InventoryOpen = true;
                }
            }
        }
    }
}
