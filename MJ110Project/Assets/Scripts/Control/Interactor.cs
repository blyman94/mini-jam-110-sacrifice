using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform modelSlotTransform;

    [Header("Interaction Parameters")]
    [SerializeField] private float interactionDistance = 1.0f;
    [SerializeField] private LayerMask interactableLayers;

    [Header("ScriptableObject References")]
    [SerializeField] private StringVariable highlightedItemName;
    [SerializeField] private StringVariable selectedItemDescription;
    [SerializeField] private GameEvent itemSelectedEvent;

    private Ray interactionRay;
    private Item lastFoundItem;

    #region MonoBehaviour Methods
    private void Update()
    {
        RaycastScan();
    }
    #endregion

    public void Interact()
    {
        Ray interactionRay =
            mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if (Physics.Raycast(interactionRay, out hit, interactionDistance,
            interactableLayers))
        {
            if (hit.collider != null)
            {
                Item foundItem = hit.collider.GetComponent<Item>();
                if (foundItem != null)
                {
                    // Debug.Log(string.Format("{0} was interacted with!",
                    //     foundItem.ItemData.Name));
                    selectedItemDescription.Value = foundItem.ItemData.Description;
                    Instantiate(foundItem.ItemData.itemPrefab, modelSlotTransform);
                    itemSelectedEvent.Raise();
                }   
            }
        }
    }

    private void RaycastScan()
    {
        Ray interactionRay =
            mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if (Physics.Raycast(interactionRay, out hit, interactionDistance,
            interactableLayers))
        {
            if (hit.collider != null)
            {
                Item foundItem = hit.collider.GetComponent<Item>();
                if (foundItem != null)
                {
                    foundItem.HighlightTimer = 0.05f;
                    highlightedItemName.Value = foundItem.ItemData.Name;
                } 
            }
        }
        else
        {
            highlightedItemName.Value = "";
        }
    }
}
