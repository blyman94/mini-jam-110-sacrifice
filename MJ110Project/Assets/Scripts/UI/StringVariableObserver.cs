using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringVariableObserver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToUpdate;
    [SerializeField] private StringVariable observedVariable;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        observedVariable.variableUpdated += UpdateDisplay;
    }
    private void OnDisable()
    {
        observedVariable.variableUpdated -= UpdateDisplay;
    }
    #endregion

    private void UpdateDisplay()
    {
        textToUpdate.text = observedVariable.Value;
    }   
}
