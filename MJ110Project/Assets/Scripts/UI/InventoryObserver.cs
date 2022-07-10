using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryObserver : MonoBehaviour
{
    [SerializeField] private Inventory observedInventory;
    [SerializeField] private Transform inventoryPreviewParent;
    [SerializeField] private GameObject itemPreviewDisplayPrefab;

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
            GameObject itemPreviewObject =
                Instantiate(itemPreviewDisplayPrefab, inventoryPreviewParent);

            Image itemPreviewImage = itemPreviewObject.GetComponent<Image>();
            itemPreviewImage.sprite = itemData.IconSprite;
        }
    }

    private void DestroyAllChildren()
    {
        for (int i = inventoryPreviewParent.childCount - 1; i >= 0; i--)
        {
            Destroy(inventoryPreviewParent.GetChild(i).gameObject);
        }
    }
}
