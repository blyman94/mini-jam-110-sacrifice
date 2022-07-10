using UnityEngine;

/// <summary>
/// Controls an AudioSource to allow for audio effects to be played in response 
/// to events. Reads which AudioClip to play from a ScriptableObject Variable.
/// </summary>
public class SoundEffectController : MonoBehaviour
{
    /// <summary>
    /// Source through which to play the sound effect.
    /// </summary>
    [SerializeField] private AudioSource soundEffectSource;

    /// <summary>
    /// ScriptableObject variable containing the AudioClip to be played.
    /// </summary>
    [SerializeField] private AudioClipVariable currentAudioClip;

    /// <summary>
    /// Uses the SoundEffectSource to play the audioclip stored in the 
    /// currentAudioClip ScriptableObject variable.
    /// </summary>
    public void PlayCurrentAudioClip()
    {
        soundEffectSource.PlayOneShot(currentAudioClip.Value);
    }
}
