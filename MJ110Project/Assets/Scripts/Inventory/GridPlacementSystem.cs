using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridPlacementSystem : MonoBehaviour
{
    [SerializeField] private MouseControl _mouseControl;
    [SerializeField] private Transform testTransform;
   private GridZ<GridObject> grid;
   
   private InputAction leftMouseClick;


   private void Awake()
   {
      int gridWidth = 10;
      int gridHeight = 10;
      float cellSize = 15;
      grid = new GridZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero,
         (GridZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
         
         leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
                 leftMouseClick.performed += ctx => LeftMouseClicked();
                 leftMouseClick.Enable();
      
   }
   
       private void LeftMouseClicked()
       {
           grid.GetXY( _mouseControl.GetMouseWorldPosition(), out int x, out int z);
            Debug.Log(x + ", " + z);
           Instantiate(testTransform,grid.GetWorldPosition(x, z), Quaternion.identity);
       } 
}
