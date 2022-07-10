using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public VariableUpdated variableUpdated;
    [SerializeField] private List<ItemData> inventoryItems;
    [SerializeField] private int maxWeight;
    public List<ItemData> InventoryItems
    {
        get
        {
            return inventoryItems;
        }
        set
        {
            inventoryItems = value;
            variableUpdated?.Invoke();
        }
    }

    public int GetInventoryWeight()
    {
        int inventoryWeight = 0;
        if (InventoryItems.Count > 0)
        {
            foreach (ItemData itemData in InventoryItems)
            {
                inventoryWeight += itemData.Weight;
            }
        }
        return inventoryWeight;
    }

    public void AddItem(ItemData itemToAdd)
    {
        if (InventoryItems.Contains(itemToAdd))
        {
            Debug.Log("Warning: This item already exists in inventory.");
        }
        int newTotalWeight = GetInventoryWeight() + itemToAdd.Weight;
        if (newTotalWeight <= maxWeight)
        {
            InventoryItems.Add(itemToAdd);
        }
    }

    public void RemoveItem(ItemData itemToRemove)
    {
        if (InventoryItems.Contains(itemToRemove))
        {
            InventoryItems.Remove(itemToRemove);
        }
    }
}
