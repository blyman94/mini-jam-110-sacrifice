using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ]
public class InventoryItem : ScriptableObject
{

   
    
    
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Transform prefab;
    [SerializeField] private Transform visual;
    [SerializeField] private string name;

    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.DOWN: return 0;
            case Dir.LEFT: return 90;
            case Dir.UP: return 180;
            case Dir.RIGHT: return 270 ;
        }
    }
    public Vector2Int GetRotationOffset(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.DOWN: return new Vector2Int(0,0);
            case Dir.LEFT: return new Vector2Int(0,width);
            case Dir.UP: return new Vector2Int(width,height);
            case Dir.RIGHT: return new Vector2Int(height,0);
        }
    }

    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        
        switch(dir) {
            
            case Dir.UP:
            case Dir.DOWN:
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x,y));
                    }
                }

                break;
            
            case Dir.LEFT:
            case Dir.RIGHT:
                for (int x = 0; x < height; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x,y));
                    }
                }
                break;
        }
        
        Debug.Log(gridPositionList.Count);
        return gridPositionList;
    }

    

        public Transform GetPrefab()
    {
        return this.prefab;
    }

        public Transform GetVisual()
        {
            return visual;
        }
}
