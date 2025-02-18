using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI gameScoreText;
    [SerializeField] private TextMeshProUGUI menuBestScoreText;

    [Header("Settings")]
    [SerializeField] private float scoreMultiplier;
    private int score;
    private int bestScore;

    [Header("Data")]
    private const string bestScoreKey = "bestScoreKey";

    private void Awake()
    {
        LoadData();

        MergeManager.onMergeProcessed += MergeProcessedCallback;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCallback;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void Start()
    {
        UpdateScoreText();
        UpdateBestScoreText();
    }

    private void MergeProcessedCallback(FruitType fruitType, Vector2 unused)
    {
        int scoreToAdd = (int)fruitType;
        score += (int)(scoreToAdd * scoreMultiplier);

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        gameScoreText.text = score.ToString();
    }

    private void UpdateBestScoreText()
    {
        menuBestScoreText.text = bestScore.ToString();
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GameOver:
                CalculateBestScore();
                break;
        }
    }

    private void CalculateBestScore()
    {
        if (score >= bestScore)
        {
            bestScore = score;
            SaveData();
        }
    }

    private void LoadData()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(bestScoreKey, bestScore);
    }
}
