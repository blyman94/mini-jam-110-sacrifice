using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectable : MonoBehaviour
{
    // enable pickingUp items (K)
    public Vector3 posToGo;
    public TetrisItem itemTetris;

    public void Collect()
    {

        bool wasPickedUpTestris = false;
        wasPickedUpTestris =
            TetrisSlot.instanceSlot.addInFirstSpace(itemTetris); //add to the bag matrix.
        if (wasPickedUpTestris) // took
        {
            Destroy(this.gameObject);
        }
    }
}


