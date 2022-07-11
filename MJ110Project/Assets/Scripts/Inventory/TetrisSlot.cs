using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSlot : MonoBehaviour
{
    //script of the inventory matrix to add/remove tetris items
    #region Singleton
    public static TetrisSlot instanceSlot;
    

    void Awake()
    {
        if (instanceSlot != null)
        {
            Debug.LogWarning("More than one Tetris inventory");
            return;
        }
        instanceSlot = this;
    }
    #endregion

    public int[,] grid;//2 dimensions

    public TetrisInventory playerInventory; //14 slots for each line
    public List<TetrisItemSlot> itensInBag = new List<TetrisItemSlot>(); // itens in bag

    public Inventory displayInventory;
    public int maxGridX;
    public int maxGridY;

    public TetrisItemSlot prefabSlot; // item prefab
    Vector2 cellSize = new Vector2(68f, 68f); //slot cell size 

    List<Vector2> posItemNaBag = new List<Vector2>(); // new item pos in bag matrix

    void Start()
    {
        playerInventory = FindObjectOfType<TetrisInventory>();

        maxGridX = 2;
        maxGridY = 2;

        grid = new int[maxGridX, maxGridY]; // matrix of bag size
    }

    public bool addInFirstSpace(TetrisItem item)
    {
        int contX = (int)item.itemSize.x; //size of item in x
        int contY = (int)item.itemSize.y; //size of item in y

        for (int i = 0; i < maxGridX ; i++)//bag in X
        {
            for (int j = 0; j < maxGridY ; j++) //bag in Y
            {
                if (posItemNaBag.Count != (contX * contY)) // if false, the item fit the bag
                {
                    //for each x,y position (i,j), test if item fits
                    for (int sizeY = 0; sizeY < contY; sizeY++) // item size in Y
                    {
                        for (int sizeX = 0; sizeX < contX; sizeX++)//item size in X
                        {
                            Debug.Log(i);
                            Debug.Log(sizeX);
                            Debug.Log(j);
                            Debug.Log(sizeY);
                            Debug.Log(grid==null);
                            if ((i + sizeX) < maxGridX && (j + sizeY) < maxGridY && grid[i + sizeX, j + sizeY] != 1)//inside of index
                            {
                                Vector2 pos;
                                pos.x = i + sizeX;
                                pos.y = j + sizeY;
                                posItemNaBag.Add(pos);
                                item.ActiveInScene = false;
                            } else {
                                sizeX = contX;
                                sizeY = contY;
                                posItemNaBag.Clear();
                            }
                        }
                    }
                } else {
                    break;
                }
            }
        }
        

        if (posItemNaBag.Count == (contX * contY)) // if item already in bag
        {
            TetrisItemSlot myItem = Instantiate(prefabSlot);
            myItem.PlayerInventory = displayInventory;
            myItem.startPosition = new Vector2(posItemNaBag[0].x, posItemNaBag[0].y); //first position
            myItem.item = item; // get item
            myItem.icon.sprite = item.IconSprite; //get icon
            myItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //change anchor position
            myItem.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
            myItem.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
            myItem.transform.SetParent(this.GetComponent<RectTransform>(), false);
            myItem.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            myItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(myItem.startPosition.x * cellSize.x, -myItem.startPosition.y * cellSize.y);
            itensInBag.Add(myItem);

            for (int k = 0; k < posItemNaBag.Count; k++) //upgrade matrix
            {
                int posToAddX = (int)posItemNaBag[k].x;
                int posToAddY = (int)posItemNaBag[k].y;
                grid[posToAddX, posToAddY] = 1;
            }
            posItemNaBag.Clear();
            Debug.Log("COunt: " + itensInBag.Count);
            return true;
        }
        return false;
    }
}
