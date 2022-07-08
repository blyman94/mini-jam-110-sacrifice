using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemNameDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private StringVariable highlightedItemName;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        highlightedItemName.variableUpdated += UpdateDisplay;
    }
    private void OnDisable()
    {
        highlightedItemName.variableUpdated -= UpdateDisplay;
    }
    #endregion

    private void UpdateDisplay()
    {
        itemNameText.text = highlightedItemName.Value;
    }   
}
