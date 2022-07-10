using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources;

    [SerializeField] private Inventory playerInventory;

    [SerializeField] private float maxMusicVolume = 0.1f;

    [SerializeField] private float fadeTime = 2;

    private List<int> activeAudioSources;

    private void Awake()
    {
        activeAudioSources = new List<int>();
    }

    private void Start()
    {
        AdjustVolumes();
    }

    private void OnEnable()
    {
        playerInventory.variableUpdated += AdjustVolumes;
    }

    private void OnDisable()
    {
        playerInventory.variableUpdated -= AdjustVolumes;
    }

    private void AdjustVolumes()
    {
        activeAudioSources = new List<int>();

        foreach (ItemData itemData in playerInventory.InventoryItems)
        {
            if (audioSources[itemData.AudioTrackIndex].volume != 0.1f)
            {
                StartCoroutine(MusicFadeIn(itemData.AudioTrackIndex));
                //audioSources[itemData.AudioTrackIndex].volume = 0.1f;
            }

            if (!activeAudioSources.Contains(itemData.AudioTrackIndex))
            {
                activeAudioSources.Add(itemData.AudioTrackIndex);
            }
        }

        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!activeAudioSources.Contains(i) && audioSources[i].volume != 0)
            {
                StartCoroutine(MusicFadeOut(i));
                //audioSources[i].volume = 0;
            }
        }
    }

    private IEnumerator MusicFadeIn(int index)
    {
        float elapsedTime = 0.0f;
        float currentVolume = audioSources[index].volume;

        while (elapsedTime < fadeTime)
        {
            audioSources[index].volume = Mathf.Lerp(currentVolume, maxMusicVolume,
                elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSources[index].volume = maxMusicVolume;
    }

    private IEnumerator MusicFadeOut(int index)
    {
        Debug.Log("Called");
        float elapsedTime = 0.0f;
        float currentVolume = audioSources[index].volume;

        while (elapsedTime < fadeTime)
        {
            audioSources[index].volume = Mathf.Lerp(currentVolume, 0.0f,
                elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSources[index].volume = 0.0f;
    }
}
