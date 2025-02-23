using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI levelIndexText;

    private void Start()
    {
        GetComponent<Image>().color = Random.ColorHSV(0f, 1f, .5f, 1f, .8f, 1f);
    }

    public void Configure(int levelIndex)
    {
        levelIndexText.text = levelIndex.ToString();
    }
}
