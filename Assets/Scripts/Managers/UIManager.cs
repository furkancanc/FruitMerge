using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        SetMenu();
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
}
