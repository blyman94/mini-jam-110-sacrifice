using UnityEngine;


public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public InventoryItem[] itemTetrisSOArray;

    public InventoryItem ammo;
    public InventoryItem grenade;
    public InventoryItem katana;
    public InventoryItem medkit;
    public InventoryItem pistol;
    public InventoryItem rifle;
    public InventoryItem shotgun;
    public InventoryItem money;

    public InventoryItem GetItemTetrisSOFromName(string itemTetrisSOName) {
        foreach (InventoryItem itemTetrisSO in itemTetrisSOArray) {
            if (itemTetrisSO.name == itemTetrisSOName) {
                return itemTetrisSO;
            }
        }
        return null;
    }


    public Sprite gridBackground;
    public Sprite gridBackground_2;
    public Sprite gridBackground_3;

    public Transform gridVisual;
    
}
