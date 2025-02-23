using System;
using UnityEngine;

public class LevelMapManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform mapContent;
    [SerializeField] private RectTransform[] levelButtonParents;
    [SerializeField] private LevelButton levelButtonPrefab;

    [Header("Data")]
    [SerializeField] private LevelDataSO[] levelDatas;

    [Header("Actions")]
    public static Action onLevelButtonClicked;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Setup the content / Scroll view / map
        mapContent.anchoredPosition = Vector2.up * 1920 * (mapContent.childCount - 1);

        CreateLevelButtons();
    }

    private void CreateLevelButtons()
    {
        for (int i = 0; i < levelDatas.Length; ++i)
        {
            CreateLevelButton(i, levelButtonParents[i]);
        } 
    }

    private void CreateLevelButton(int buttonIndex, Transform levelButtonParent)
    {
        LevelButton levelButton = Instantiate(levelButtonPrefab, levelButtonParent);
        levelButton.Configure(buttonIndex);

        levelButton.GetButton().onClick.AddListener(() => LevelButtonClicked(buttonIndex));
    }

    private void LevelButtonClicked(int buttonIndex)
    {
        while (transform.childCount > 0)
        {
            Transform t = transform.GetChild(0);
            t.SetParent(null);
            Destroy(t.gameObject);
        }

        // Spawn the level prefab
        Instantiate(levelDatas[buttonIndex].GetLevel(), transform);

        // Start the game
        onLevelButtonClicked?.Invoke();
    }
}
