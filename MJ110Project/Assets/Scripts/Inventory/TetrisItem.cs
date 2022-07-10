using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Item", menuName = "Iventory/Tetris/Item")]
public class TetrisItem : ScriptableObject
{
    //Parent tree node (parent of all items)

    //Basic information
    public string itemID; 
    public Sprite itemIcon;
    public string itemName;
    public string itemDescription;
    public bool usable;
    public int currentStackSize;
    public int MaxStackSize;
    public Vector2 itemSize; //x and y
    public string rarity;

    [SerializeField]
    protected  int att1;
    [SerializeField]
    protected Sprite att1_icon;


   public int getAtt1()
    {
        return att1;

    }

    public Sprite getAtt1Icon()
    {
        return att1_icon;
    }

    public virtual void Use() //to be overrided.
    {
        Debug.Log("using");
    }
}
