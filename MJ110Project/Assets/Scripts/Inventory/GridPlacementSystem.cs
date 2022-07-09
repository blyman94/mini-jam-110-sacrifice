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
   
   private InputAction leftMouseClick;

   private InputAction rotateButton;
   private InputAction rightMouseClick;

   private Dir dir = Dir.DOWN;

   private void Awake()
   { 
       Instance = this;
      int gridWidth = 3;
      int gridHeight = 3;
      float cellSize = 15;
      grid = new GridZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero,
         (GridZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
         
     leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
     leftMouseClick.performed += ctx => Build();
     leftMouseClick.Enable();
     
     rightMouseClick = new InputAction(binding: "<Mouse>/rightButton");
     rightMouseClick.performed += ctx => Clear();
     rightMouseClick.Enable();
     
     rotateButton = new InputAction(binding: "<Keyboard>/space");
     rotateButton.performed += ctx => rotateItem();
     rotateButton.Enable();
   }

   //BAD CODE, REFACTOR PLEASE
   public Vector3 GetMouseWorldSnappedPosition()
   {
       grid.GetXY(_mouseControl.GetMouseWorldPosition(), out int x,out int z);
       return new Vector3(x, 0, z);
   }
   
   public Quaternion GetPlacedObjectRotation()
   {
       float rotate = item.GetRotationAngle(dir);
       return new Quaternion(0f, rotate, 0f, 0f);

   }
   private void rotateItem()
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

   private void Clear()
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


  
       private void Build()
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
                                        new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.getCellSize();
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
