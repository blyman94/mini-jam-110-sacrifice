using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisInventory : MonoBehaviour
{
    //Responsible for having just one inventory in the scene.
    #region Singleton
    public static TetrisInventory instanceTetris;

    void Awake()
    {
        if (instanceTetris != null)
        {
            Debug.LogWarning("More than one Tetris inventory");
            return;
        }
        instanceTetris = this;
    }
    
    #endregion

    public int numberSlots; // starts with one + the number you put.

}
