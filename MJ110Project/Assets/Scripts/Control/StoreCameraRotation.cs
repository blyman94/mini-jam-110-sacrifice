using UnityEngine;

[RequireComponent(typeof(Camera))]
public class StoreCameraRotation : MonoBehaviour
{
    [SerializeField] private QuaternionVariable storageVariable;

    private Camera targetCamera;

    private Transform targetCameraTransform;

    #region MonoBehaviour Methods
    private void Awake()
    {
        targetCamera = GetComponent<Camera>();
        targetCameraTransform = targetCamera.transform;
    }
    private void Update()
    {
        if (targetCameraTransform.rotation != storageVariable.Value)
        {
            storageVariable.Value = targetCameraTransform.rotation;
        }
    }
    #endregion
}
