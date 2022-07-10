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
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private StringVariable highlightedItemName;
    [SerializeField] private StringVariable selectedItemName;
    [SerializeField] private StringVariable selectedItemDescription;
    [SerializeField] private AudioClipVariable currentAudioClip;
    [SerializeField] private GameEvent itemSelectedEvent;
    [SerializeField] private GameEvent doorClickedEvent;

    private TetrisItem potentialItem;

    private Ray interactionRay;
    private Item lastFoundItem;
    public TetrisItem itemTetris;


    #region MonoBehaviour Methods
    private void Update()
    {
        RaycastScan();
    }
    #endregion

    public void TryAddToInventory()
    {
        bool itemAdded = playerInventory.AddItem(potentialItem);
        TetrisSlot.instanceSlot.addInFirstSpace(potentialItem); //add to the bag matrix.

        if (itemAdded)
        {
            //Debug.Log("Item added successfully.");
        }
        else
        {
            //Debug.Log("Item add failed.");
        }
    }

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
                    if (foundItem.ItemData.Name == "Exit")
                    {
                        doorClickedEvent.Raise();
                        return;
                    }
                    // Debug.Log(string.Format("{0} was interacted with!",
                    //     foundItem.ItemData.Name));
                    selectedItemDescription.Value = foundItem.ItemData.Description;
                    selectedItemName.Value = foundItem.ItemData.Name;
                    Instantiate(foundItem.ItemData.itemPrefab, modelSlotTransform);
                    potentialItem = foundItem.ItemData;
                    currentAudioClip.Value = foundItem.ItemData.pickupSFXClip;
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
