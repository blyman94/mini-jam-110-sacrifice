using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable.../String")]
public class StringVariable : ScriptableObject
{
    public VariableUpdated variableUpdated;
    private string value;

    public string Value
    {
        get
        {
            return this.value;
        }
        set
        {
            this.value = value;
            variableUpdated?.Invoke();
        }
    }
}
