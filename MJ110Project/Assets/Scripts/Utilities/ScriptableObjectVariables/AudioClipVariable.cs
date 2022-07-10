using UnityEngine;

/// <summary>
/// ScriptableObject representation of an Audio Clip.
/// </summary>
[CreateAssetMenu(menuName = "Variable.../Audio Clip Variable")]
public class AudioClipVariable : ScriptableObject
{
    /// <summary>
    /// AudioClip value of the variable.
    /// </summary>
    public AudioClip Value;
}
