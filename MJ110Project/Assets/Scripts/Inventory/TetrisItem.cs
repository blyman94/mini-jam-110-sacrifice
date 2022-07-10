using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Item", menuName = "Iventory/Tetris/Item")]
public class TetrisItem : ItemData
{
    //Parent tree node (parent of all items)

    //Basic information
    public Sprite itemIcon;
    public string itemName;
    public string itemDescription;
    public Vector2 itemSize; //x and y





    public virtual void Use() //to be overrided.
    {
        Debug.Log("using");
    }
}
