using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public bool IsInMenu { get; set; }

    [SerializeField] private Transform transformToRotate;

    [SerializeField] private float speed;

    #region MonoBehaviour Methods
    public void Start()
    {
        IsInMenu = false;
    }

    public void Update()
    {
        if (IsInMenu)
        {
            if (Input.GetMouseButton(0)) 
            {
                transformToRotate.Rotate(new Vector3(Input.GetAxis("Mouse X"),
                    Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * speed);
            }
        }
    }
    #endregion
}
