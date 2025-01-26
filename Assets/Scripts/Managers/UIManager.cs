using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void Start()
    {
        SetMenu();
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                SetMenu();
                break;
            case GameState.Game:
                SetGame();
                break;
            case GameState.GameOver:
                SetGameOver();
                break;
        }
    }

    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void SetGame()
    {
        gamePanel.SetActive(true);
        menuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void SetGameOver()
    {
        gameOverPanel.SetActive(true);
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    public void PlayButtonCallback()
    {
        GameManager.instance.SetGameState();
        SetGame();
    }

    public void NextButtonCallback()
    {
        SceneManager.LoadScene(0);
    }
}
