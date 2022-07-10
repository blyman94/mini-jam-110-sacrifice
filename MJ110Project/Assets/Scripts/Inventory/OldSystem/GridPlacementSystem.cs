using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridPlacementSystem : MonoBehaviour
{

    public static GridPlacementSystem Instance; 
    [SerializeField] private MouseControl _mouseControl;
    [SerializeField] private InventoryItem item;
   private GridZ<GridObject> grid;
   


   private Dir dir = Dir.DOWN;

   private void Awake()
   { 
       Instance = this;
      int gridWidth = 3;
      int gridHeight = 3;
      float cellSize = 15;
      grid = new GridZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero,
         (GridZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
         

   }


   public void rotateItem()
   {
       dir = Extensions.getNextDir(dir);
       Debug.Log(dir);
   }

   private PlacedObject PlaceObject(Vector3 worldPosition, Vector2Int origin, Dir dir, InventoryItem SO)
   {
       Transform buildTransform = Instantiate(
           SO.GetPrefab(),worldPosition, 
           Quaternion.Euler(0,SO.GetRotationAngle(dir), 0));
       PlacedObject placedObject = buildTransform.AddComponent<PlacedObject>();
       placedObject.SetDir(dir);
       placedObject.SetOrigin(origin);
       placedObject.SetSO(SO);

       return placedObject;
   }

   public void Clear()
   {
       GridObject obj = grid.GetGridObject(_mouseControl.GetMouseWorldPosition());
       PlacedObject placedObject = obj.GetPlacedObject();
       if (placedObject != null)
       {
           placedObject.DestroySelf();
       }

       List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
       foreach (Vector2Int gridPosition in gridPositionList)
       {
           grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
       }
   }

   //BAD CODE, REFACTOR PLEASE
   public Vector2Int GetGridPosition(Vector3 worldPosition) {
       grid.GetXY(worldPosition, out int x, out int z);
       return new Vector2Int(x, z);
   }

   public bool IsValidGridPosition(Vector2Int gridPosition)
   {
       return grid.IsValidGridPosition(gridPosition);
   }

   public GridZ<GridObject> GetGrid()
   {
       return grid;
       
   }


   public Vector3 GetMouseWorldSnappedPosition() {
       Vector3 mousePosition = _mouseControl.GetMouseWorldPosition();
       grid.GetXY(mousePosition, out int x, out int y);

       if (item != null) {
           Vector2Int rotationOffset = item.GetRotationOffset(dir);
           Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y) * grid.getCellSize();
           return placedObjectWorldPosition;
       } else {
           return mousePosition;
       }
   }

   public Quaternion GetPlacedObjectRotation() {
       if (item != null) {
           return Quaternion.Euler(0, 0, -item.GetRotationAngle(dir));
       } else {
           return Quaternion.identity;
       }
   }

   public InventoryItem GetPlacedObjectTypeSO() {
       return item;
   }

  
   public void Build()
       {
           grid.GetXY( _mouseControl.GetMouseWorldPosition(), out int x, out int z);
           
           List<Vector2Int> gridPositionList = item.GetGridPositionList(new Vector2Int(x, z), dir);

           bool canBuild = true;
           
           foreach (Vector2Int gridPosition in gridPositionList)
           {
               try
               {
                   
                   if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                   {
                       canBuild = false;
                       break;
                   }
               }
               catch (NullReferenceException e)
               {
                   Debug.Log("No Tile Here");
                   return;
               }
          
               
           }
           
           if(canBuild && x < grid.GetWidth() && x >= 0 && z < grid.GetHeight() && z >= 0)
           {
               Vector2Int rotationOffset = item.GetRotationOffset(dir);
               Vector3 placedPosition = grid.GetWorldPosition(x, z) +
                                        new Vector3(rotationOffset.x, rotationOffset.y) * grid.getCellSize();
               PlacedObject placedObject = PlaceObject(placedPosition,new Vector2Int(x,z),dir,item);
               foreach (Vector2Int gridPosition in gridPositionList)
               {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
               }
           }
           else
           {
               Debug.Log("");
           }
       } 
}
