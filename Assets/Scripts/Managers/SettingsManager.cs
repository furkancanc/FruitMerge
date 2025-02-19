using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject resetProgressPrompt;
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
}
