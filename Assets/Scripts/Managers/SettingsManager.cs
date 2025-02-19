using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject resetProgressPrompt;
    [SerializeField] private Slider pushMagnitudeSlider;
    [SerializeField] private Toggle sfxToggle;

    [Header("Actions")]
    public static Action<float> onPushMagnitudeChanged;
    public static Action<bool> onSFXValueChanged;

    [Header("Data")]
    private const string lastPushMagnitudeKey = "lastPushMagnitude";
    private const string sfxActiveKey= "sfxActiveKey";
    private bool canSave;

    private void Awake()
    {
        LoadData();
    }

    IEnumerator Start()
    {
        // Initialize the push magnitude value for the merge push effect
        Initialize();

        yield return new WaitForSeconds(.5f);
        canSave = true;
    }

    private void Initialize()
    {
        onPushMagnitudeChanged?.Invoke(pushMagnitudeSlider.value);
        onSFXValueChanged?.Invoke(sfxToggle.isOn);
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

    public void ToggleCallback(bool sfxActive)
    {
        sfxToggle.isOn = sfxActive;
        onSFXValueChanged?.Invoke(sfxActive);

        SaveData();
    }

    private void LoadData()
    {
        pushMagnitudeSlider.value = PlayerPrefs.GetFloat(lastPushMagnitudeKey);
        sfxToggle.isOn = PlayerPrefs.GetInt(sfxActiveKey) == 1; 
    }

    private void SaveData()
    {
        if (!canSave)
            return;

        PlayerPrefs.SetFloat(lastPushMagnitudeKey, pushMagnitudeSlider.value);
        PlayerPrefs.SetInt(sfxActiveKey, sfxToggle.isOn ? 1 : 0);
    }
}
