
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisInventorySystem : MonoBehaviour
{
   public static TetrisInventorySystem Instance { get; private set; }


    public event EventHandler<PlacedObject> OnObjectPlaced;

    private GridZ<GridObject> grid;
    private RectTransform itemContainer;
       
   [SerializeField] private int gridWidth = 10;
   [SerializeField] private int gridHeight = 10;

    private void Awake() {
        Instance = this;

 
        float cellSize = 50f;
        grid = new GridZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridZ<GridObject> g, int x, int y) => new GridObject(g, x, y));

        itemContainer = transform.Find("ItemContainer").GetComponent<RectTransform>();

    }

    public class GridObject {

        private GridZ<GridObject> grid;
        private int x;
        private int y;
        public PlacedObject placedObject;

        public GridObject(GridZ<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        public override string ToString() {
            return x + ", " + y + "\n" + placedObject;
        }

        public void SetPlacedObject(PlacedObject placedObject) {
            this.placedObject = placedObject;
            grid.TriggerOnGridValueChanged(x, y);
        }

        public void ClearPlacedObject() {
            placedObject = null;
            grid.TriggerOnGridValueChanged(x, y);
        }

        public PlacedObject GetPlacedObject() {
            return placedObject;
        }

        public bool CanBuild() {
            return placedObject == null;
        }

        public bool HasPlacedObject() {
            return placedObject != null;
        }

    }

    public GridZ<GridObject> GetGrid() {
        return grid;
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        grid.GetXY(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public bool IsValidGridPosition(Vector2Int gridPosition) {
        return grid.IsValidGridPosition(gridPosition);
    }

        public PlacedObject CreateCanvas(Transform parent, Vector2 anchoredPosition, Vector2Int origin, Dir dir, InventoryItem placedObjectTypeSO) {
            Transform placedObjectTransform = Instantiate(placedObjectTypeSO.GetPrefab(), parent);
            placedObjectTransform.rotation = Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
            placedObjectTransform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

            PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
            placedObject.SetSO(placedObjectTypeSO);
            placedObject.SetOrigin(origin);
            placedObject.SetDir(dir);
            return placedObject;
    }
    
    public bool TryPlaceItem(InventoryItem itemTetrisSO, Vector2Int placedObjectOrigin, Dir dir) {
        // Test Can Build
        List<Vector2Int> gridPositionList = itemTetrisSO.GetGridPositionList(placedObjectOrigin, dir);
        bool canPlace = true;
        foreach (Vector2Int gridPosition in gridPositionList) {
            bool isValidPosition = grid.IsValidGridPosition(gridPosition);
            if (!isValidPosition) {
                // Not valid
                canPlace = false;
                break;
            }
            if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                canPlace = false;
                break;
            }
        }

        if (canPlace) {
            foreach (Vector2Int gridPosition in gridPositionList) {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                    canPlace = false;
                    break;
                }
            }
        }

        if (canPlace) {
            Vector2Int rotationOffset = itemTetrisSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, rotationOffset.y) * grid.getCellSize();

            PlacedObject placedObject = CreateCanvas(itemContainer, placedObjectWorldPosition, placedObjectOrigin, dir, itemTetrisSO);
            placedObject.transform.rotation = Quaternion.Euler(0, 0, -itemTetrisSO.GetRotationAngle(dir));

            placedObject.GetComponent<TetrisDragDrop>().Setup(this);

            foreach (Vector2Int gridPosition in gridPositionList) {
                grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }

            OnObjectPlaced?.Invoke(this, placedObject);

            // Object Placed!
            return true;
        } else {
            // Object CANNOT be placed!
            return false;
        }
    }

    public void RemoveItemAt(Vector2Int removeGridPosition) {
        PlacedObject placedObject = grid.GetGridObject(removeGridPosition.x, removeGridPosition.y).GetPlacedObject();

        if (placedObject != null) {
            // Demolish
            placedObject.DestroySelf();

            List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
            foreach (Vector2Int gridPosition in gridPositionList) {
                grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
            }
        }
    }

    public RectTransform GetItemContainer() {
        return itemContainer;
    }

    public static void CreateVisualGrid(Transform visualParentTransform, InventoryItem itemTetrisSO, float cellSize) {
        Transform visualTransform = Instantiate(AssetManager.Instance.gridVisual, visualParentTransform);

        // Create background
        Transform template = visualTransform.Find("Template");
        template.gameObject.SetActive(false);

        for (int x = 0; x < itemTetrisSO.GetWidth(); x++) {
            for (int y = 0; y < itemTetrisSO.GetHeight(); y++) {
                Transform backgroundSingleTransform = Instantiate(template, visualTransform);
                backgroundSingleTransform.gameObject.SetActive(true);
            }
        }

        visualTransform.GetComponent<GridLayoutGroup>().cellSize = Vector2.one * cellSize;

        visualTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(itemTetrisSO.GetWidth(), itemTetrisSO.GetHeight()) * cellSize;

        visualTransform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        visualTransform.SetAsFirstSibling();
    }


    [Serializable]
    public struct AddItemTetris {
        public string itemTetrisSOName;
        public Vector2Int gridPosition;
        public Dir dir;
    }

    [Serializable]
    public struct ListAddItemTetris {
        public List<AddItemTetris> addItemTetrisList;
    }

    public string Save() {
        List<PlacedObject> placedObjectList = new List<PlacedObject>();
        for (int x = 0; x < grid.GetWidth(); x++) {
            for (int y = 0; y < grid.GetHeight(); y++) {
                if (grid.GetGridObject(x, y).HasPlacedObject()) {
                    placedObjectList.Remove(grid.GetGridObject(x, y).GetPlacedObject());
                    placedObjectList.Add(grid.GetGridObject(x, y).GetPlacedObject());
                }
            }
        }

        List<AddItemTetris> addItemTetrisList = new List<AddItemTetris>();
        foreach (PlacedObject placedObject in placedObjectList) {
            addItemTetrisList.Add(new AddItemTetris {
                dir = placedObject.GetDir(),
                gridPosition = placedObject.GetGridPosition(),
                itemTetrisSOName = (placedObject.GetSO()).name,
            });

        }

        return JsonUtility.ToJson(new ListAddItemTetris { addItemTetrisList = addItemTetrisList });
    }

    public void Load(string loadString) {
        ListAddItemTetris listAddItemTetris = JsonUtility.FromJson<ListAddItemTetris>(loadString);

        foreach (AddItemTetris addItemTetris in listAddItemTetris.addItemTetrisList) {
            TryPlaceItem(AssetManager.Instance.GetItemTetrisSOFromName(addItemTetris.itemTetrisSOName), addItemTetris.gridPosition, addItemTetris.dir);
        }
    }

}
