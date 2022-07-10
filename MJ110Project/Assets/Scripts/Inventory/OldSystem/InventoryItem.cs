using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu ]
public class InventoryItem : ScriptableObject
{

    public static void CreateVisualGrid(Transform visualParentTransform, InventoryItem itemTetrisSO, float cellSize) {
        Transform visualTransform = Instantiate(AssetManager.Instance.gridVisual, visualParentTransform);

        // Create background
        Transform template = visualTransform.Find("Template");
        template.gameObject.SetActive(false);

        for (int x = 0; x < itemTetrisSO.width; x++) {
            for (int y = 0; y < itemTetrisSO.height; y++) {
                Transform backgroundSingleTransform = Instantiate(template, visualTransform);
                backgroundSingleTransform.gameObject.SetActive(true);
            }
        }

        visualTransform.GetComponent<GridLayoutGroup>().cellSize = Vector2.one * cellSize;

        visualTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(itemTetrisSO.width, itemTetrisSO.height) * cellSize;

        visualTransform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        visualTransform.SetAsFirstSibling();
    }
    
    
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

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }
}
