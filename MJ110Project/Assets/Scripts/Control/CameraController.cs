using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float clampYMin = -35;
    [SerializeField] private float clampYMax = 35;
    [SerializeField] private MenuModelRotator menuModelRotator;
    [SerializeField] private InventoryToggle inventoryToggle;

    [SerializeField] private bool invertYAxis = false;
    [SerializeField] private float lookSensitivityX = 1;
    [SerializeField] private float lookSensitivityY = 1;

    private float mouseX;
    private float mouseY;
    private float prevMouseX;

    private void LateUpdate()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        if (!menuModelRotator.IsInMenu && !inventoryToggle.InventoryOpen)
        {
            mouseX += Input.GetAxis("Mouse X") * lookSensitivityY;
            mouseY += Input.GetAxis("Mouse Y") * lookSensitivityX * (invertYAxis ? 1 : -1);
            mouseY = Mathf.Clamp(mouseY, clampYMin, clampYMax);

            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            prevMouseX = mouseX;
        }
    }
}
