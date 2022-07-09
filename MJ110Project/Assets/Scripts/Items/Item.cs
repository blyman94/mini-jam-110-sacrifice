using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData ItemData;
    [SerializeField] private GameObject activeEffectObject;

    private bool isActive;

    public float HighlightTimer { get; set; }

    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            if (isActive)
            {
                Highlight();
            }
            else
            {
                StopHighlight();
            }
        }
    }

    #region MonoBehaviour Methods
    private void Start()
    {
        activeEffectObject.SetActive(false);
    }
    private void Update()
    {
        if (HighlightTimer > 0.0f)
        {
            if (!IsActive)
            {
                IsActive = true;
            }
            HighlightTimer -= Time.deltaTime;
        }
        else
        {
            IsActive = false;
        }
    }
    #endregion

    public void Highlight()
    {
        activeEffectObject.SetActive(true);
    }

    public void StopHighlight()
    {
        activeEffectObject.SetActive(false);
    }
}
