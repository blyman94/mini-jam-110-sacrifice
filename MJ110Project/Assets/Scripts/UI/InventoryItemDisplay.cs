using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemDisplay : MonoBehaviour
{
    public Inventory PlayerInventory { get; set; }
    public ItemData RepresentedItem { get; set; }

    public void PutItemBack()
    {
        //Debug.Log("Called");
        PlayerInventory.RemoveItem(RepresentedItem);
        RepresentedItem.ActiveInScene = true;
    }
}
