using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public TetrisItem ItemData;
    [SerializeField] private BoxCollider itemCollider;
    [SerializeField] private GameObject graphics;
    [SerializeField] private GameObject activeEffectObject;

    private bool isHighlighted;

    public float HighlightTimer { get; set; }

    public bool IsHighlighted
    {
        get
        {
            return isHighlighted;
        }
        set
        {
            isHighlighted = value;
            if (isHighlighted)
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
    private void OnEnable()
    {
        ItemData.SceneStateUpdated += UpdateSceneState;
    }
    private void OnDisable()
    {
        ItemData.SceneStateUpdated -= UpdateSceneState;
    }
    private void Update()
    {
        if (HighlightTimer > 0.0f)
        {
            if (!IsHighlighted)
            {
                IsHighlighted = true;
            }
            HighlightTimer -= Time.deltaTime;
        }
        else
        {
            IsHighlighted = false;
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

    private void UpdateSceneState()
    {
        if (ItemData.ActiveInScene)
        {
            itemCollider.enabled = true;
            graphics.SetActive(true);
        }
        else
        {
            itemCollider.enabled = false;
            graphics.SetActive(false);
            activeEffectObject.SetActive(false);
        }
    }
}
