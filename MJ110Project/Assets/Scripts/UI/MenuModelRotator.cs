using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModelRotator : MonoBehaviour
{
    private bool isInMenu;

    public bool IsInMenu
    {
        get
        {
            return isInMenu;
        }
        set
        {
            isInMenu = value;
            if (isInMenu)
            {
                transformToRotate.rotation = Quaternion.identity;
            }
        }
    }

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
                transformToRotate.Rotate(new Vector3(Input.GetAxis("Mouse Y"),
                    -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
            }
        }
    }
    #endregion
}
