using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI gameScoreText;

    [Header("Settings")]
    [SerializeField] private float scoreMultiplier;
    private int score;

    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCallback;
    }

    private void Start()
    {
        UpdateScore();
    }

    private void MergeProcessedCallback(FruitType fruitType, Vector2 unused)
    {
        int scoreToAdd = (int)fruitType;
        score += (int)(scoreToAdd * scoreMultiplier);

        UpdateScore();
    }

    private void UpdateScore()
    {
        gameScoreText.text = score.ToString();
    }
}
