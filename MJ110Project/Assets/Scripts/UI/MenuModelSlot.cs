using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModelSlot : MonoBehaviour
{
    [SerializeField] private Transform modelSlotTransform;

    #region MonoBehaviour Methods
    private void Start()
    {
        DeactivateModelSlot();
    }
    #endregion

    public void ActivateModelSlot()
    {
        modelSlotTransform.gameObject.SetActive(true);
    }

    public void DeactivateModelSlot()
    {
        if (modelSlotTransform.childCount > 0)
        {
            Destroy(modelSlotTransform.GetChild(0).gameObject);
        }
        modelSlotTransform.gameObject.SetActive(false);
    }
}
