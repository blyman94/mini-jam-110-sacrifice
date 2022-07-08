
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class MouseControl : ScriptableObject
{
    [SerializeField] private LayerMask mouseColliderLayerMask;
    InputDevice mouse = Mouse.current;

    public Vector3 GetMouseWorldPosition()
    {
     
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            Debug.Log(raycastHit.point);
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
            
        }

    }
}
