using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TetrisUI : MonoBehaviour
{
    //create the slots background grid based on inventory size

    TetrisInventory playerInventory;
    [SerializeField]
    GameObject slotPrefab; //empty slot prefab to create the BG.

    void Start()
    {
        playerInventory = TetrisInventory.instanceTetris;

        for (int i = 0; i < playerInventory.numberSlots; i++)
        {
            var itemUI = Instantiate(slotPrefab, transform);  //generate the slots grid.
        }
    }
    
}
