using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public VariableUpdated variableUpdated;
    public List<ItemData> InventoryItems;
    [SerializeField] private int maxWeight;

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

    public bool AddItem(ItemData itemToAdd)
    {
        if (InventoryItems.Contains(itemToAdd))
        {
            Debug.Log("Warning: This item already exists in inventory.");
            itemToAdd.ActiveInScene = true;
            return false;
        }

        if (itemToAdd.Weight == -1)
        {
            // TODO: Signal the player that they cant take this item.
            Debug.Log("I can't keep that item!");
            itemToAdd.ActiveInScene = true;
            return false;
        }

        int newTotalWeight = GetInventoryWeight() + itemToAdd.Weight;
        if (newTotalWeight <= maxWeight)
        {
            InventoryItems.Add(itemToAdd);
            variableUpdated?.Invoke();
            itemToAdd.ActiveInScene = false;
            return true;
        }
        else
        {
            // TODO: Signal the player that the item is too heavy.
            Debug.Log("Not enough space!");
            itemToAdd.ActiveInScene = true;
            return false;
        }
    }

    public void RemoveItem(ItemData itemToRemove)
    {
        if (InventoryItems.Contains(itemToRemove))
        {
            InventoryItems.Remove(itemToRemove);
            variableUpdated?.Invoke();
        }
    }
}
