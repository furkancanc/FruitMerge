using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private AudioSource mergeSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] mergeClips;
    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessedCallback;
        SettingsManager.onSFXValueChanged += SFXValueChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCallback;
        SettingsManager.onSFXValueChanged += SFXValueChangedCallback;
    }

    private void MergeProcessedCallback(FruitType _, Vector2 __)
    {
        PlayMergeSound();
    }

    public void PlayMergeSound()
    {
        //mergeSource.pitch = Random.Range(.9f, 1.1f);
        mergeSource.clip = mergeClips[Random.Range(0, mergeClips.Length)];
        mergeSource.Play();
    }

    private void SFXValueChangedCallback(bool sfxActive)
    {
        mergeSource.mute = !sfxActive;
        //mergeSource.volume = sfxActive ? 1 : 0;
    }
}
