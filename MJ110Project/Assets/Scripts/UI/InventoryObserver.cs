using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryObserver : MonoBehaviour
{
    [SerializeField] private Inventory observedInventory;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private GameObject uiPrefab;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        observedInventory.variableUpdated += UpdateInventoryDisplay;
    }
    // Testing:
    private void Start()
    {
        UpdateInventoryDisplay();
    }
    // end testing
    private void OnDisable()
    {
        observedInventory.variableUpdated -= UpdateInventoryDisplay;
    }
    #endregion

    private void UpdateInventoryDisplay()
    {
        DestroyAllChildren();
        foreach (ItemData itemData in observedInventory.InventoryItems)
        {
            GameObject itemObject =
                Instantiate(uiPrefab, inventoryParent);

            Image itemImage = itemObject.GetComponent<Image>();
            if (itemImage == null)
            {
                // We know we are working with the inventory screen here.
                itemImage = 
                    itemObject.transform.GetChild(1).GetComponent<Image>();

                InventoryItemDisplay iid = itemObject.GetComponent<InventoryItemDisplay>();
                iid.RepresentedItem = itemData;
                iid.PlayerInventory = observedInventory;
            }
            itemImage.sprite = itemData.IconSprite;
        }
    }

    private void DestroyAllChildren()
    {
        for (int i = inventoryParent.childCount - 1; i >= 0; i--)
        {
            Destroy(inventoryParent.GetChild(i).gameObject);
        }
    }
}
