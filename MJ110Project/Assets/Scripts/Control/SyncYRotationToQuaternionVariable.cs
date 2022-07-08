using UnityEngine;

public class SyncYRotationToQuaternionVariable : MonoBehaviour
{
    [SerializeField] private QuaternionVariable syncVariable;
    [SerializeField] private Transform transformToSync;

    #region MonoBehaviour Methods
    private void Update()
    {
        if (transformToSync.rotation.eulerAngles.y != 
            syncVariable.Value.eulerAngles.y)
        {
            transformToSync.rotation = 
                Quaternion.Euler(0.0f, syncVariable.Value.eulerAngles.y, 0.0f);
        }
    }
    #endregion
}
