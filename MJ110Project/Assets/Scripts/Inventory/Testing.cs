using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame 

    [SerializeField] private MouseControl controller;
 //   private GridZ<bool> gridArray;
    private void Start()
    {
        
    }

    public bool getValue()
    {
        return false;
    }
    
    private InputAction leftMouseClick;
    
    private void Awake() {
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.performed += ctx => LeftMouseClicked();
        leftMouseClick.Enable();
    }

    
    // Update is called once per frame
    private void LeftMouseClicked()
    {
            
     
    } 
    
    
    


}
