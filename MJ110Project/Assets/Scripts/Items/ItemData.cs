using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string Name = "New Item";

    [TextArea(5,5)]
    public string Description = "This is the item's description...";

    public GameObject itemPrefab;

    public int Weight;

    public Sprite IconSprite;
}
