using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    private InventoryItem SO;
    private Vector2Int origin;
    private Dir dir;

    public void SetSO(InventoryItem SO)
    {
        this.SO = SO;
    }
    
    public void SetOrigin(Vector2Int origin)
    {
        this.origin = origin;
    }
    
    public void SetDir(Dir dir)
    {
        this.dir = dir;
    }
    
    public InventoryItem GetSO()
    {
        return this.SO;
    }
    
    public Vector2Int GetGridPosition()
    {
        return origin;
    }
    
    
    
    public Dir GetDir()
    {
        return dir;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public List<Vector2Int> GetGridPositionList()
    {
        return SO.GetGridPositionList(origin, dir);
    }
    


    
}
