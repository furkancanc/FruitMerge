using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject resetProgressPrompt;
    [SerializeField] private Slider pushMagnitudeSlider;

    [Header("Actions")]
    public static Action<float> onPushMagnitudeChanged;

    [Header("Data")]
    private const string lastPushMagnitudeKey = "lastPushMagnitude";

    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        // Initialize the push magnitude value for the merge push effect
        SliderValueChangedCallback();
    }

    public void ResetProgressCallback()
    {
        resetProgressPrompt.SetActive(true);
    }

    public void ResetProgressYes()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void ResetProgressNo()
    {
        resetProgressPrompt.SetActive(false);
    }

    public void SliderValueChangedCallback()
    {
        onPushMagnitudeChanged?.Invoke(pushMagnitudeSlider.value);

        SaveData();
    }

    private void LoadData()
    {
        pushMagnitudeSlider.value = PlayerPrefs.GetFloat(lastPushMagnitudeKey);
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(lastPushMagnitudeKey, pushMagnitudeSlider.value);
    }
}
