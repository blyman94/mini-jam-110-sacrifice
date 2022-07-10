using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Mover3D Mover3D;
    public Interactor Interactor;
    public Inventory Inventory;

    #region MonoBehaviour Methods
    private void Awake()
    {
        Inventory.InventoryItems.Clear();
    }
    #endregion
}
