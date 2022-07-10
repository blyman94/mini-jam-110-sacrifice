using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public VariableUpdated variableUpdated;
    public List<ItemData> InventoryItems;
    [SerializeField] private int maxWeight;
    [SerializeField] private int maxCount;

    [SerializeField] private GameEvent itemPickupFailedWeightEvent;
    [SerializeField] private GameEvent itemPickupFailedNAEvent;

    public float GetInventoryWeight()
    {
        float inventoryWeight = 0;
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

        if (InventoryItems.Count == maxCount)
        {
            itemPickupFailedWeightEvent.Raise();
            itemToAdd.ActiveInScene = true;
            return false;
        }

        if (itemToAdd.Weight == -1)
        {
            itemPickupFailedNAEvent.Raise();
            itemToAdd.ActiveInScene = true;
            return false;
        }

        float newTotalWeight = GetInventoryWeight() + itemToAdd.Weight;
        if (newTotalWeight <= maxWeight)
        {
            InventoryItems.Add(itemToAdd);
            variableUpdated?.Invoke();
            itemToAdd.ActiveInScene = false;
            return true;
        }
        else
        {
            itemPickupFailedWeightEvent.Raise();
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
