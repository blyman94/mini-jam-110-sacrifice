using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisListItens : MonoBehaviour
{
    //all items that will be in game, to could drop them later without having the prefab.
    public GameObject[] prefabs;
    public List<TetrisItem> itens = new List<TetrisItem>();

    void Start()
    {
        for(int i = 0; i < prefabs.Length; i++)
        {
            itens.Add(prefabs[i].GetComponent<collectable>().itemTetris);
        }
    }

}
