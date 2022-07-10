using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{

    [SerializeField]
    private GridPlacementSystem system;
private InputAction leftMouseClick;

private InputAction rotateButton;
private InputAction rightMouseClick;
    
    private void Awake()
    {
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.performed += ctx => system.Build();
        leftMouseClick.Enable();
     
        rightMouseClick = new InputAction(binding: "<Mouse>/rightButton");
        rightMouseClick.performed += ctx => system.Clear();
        rightMouseClick.Enable();
     
        rotateButton = new InputAction(binding: "<Keyboard>/space");
        rotateButton.performed += ctx => system.rotateItem();
        rotateButton.Enable();
    }
}
