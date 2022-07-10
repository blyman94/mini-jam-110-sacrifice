using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public VariableUpdated SceneStateUpdated;
    public string Name = "New Item";

    [TextArea(10,10)]
    public string Description = "This is the item's description...";

    public GameObject itemPrefab;

    public int Weight;

    public Sprite IconSprite;

    [SerializeField] bool activeInScene = true;

    public bool ActiveInScene
    {
        get
        {
            return activeInScene;
        }
        set
        {
            activeInScene = value;
            SceneStateUpdated?.Invoke();
        }
    }
}
