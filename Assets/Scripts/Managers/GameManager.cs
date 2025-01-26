using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header(" Settings ")]
    private GameState gameState;

    private void Start()
    {
        SetMenu();
    }

    private void SetMenu()
    {
        gameState = GameState.Menu;
    }

    private void SetGame()
    {
        gameState = GameState.Game;
    }

    private void SetGameOver()
    {
        gameState = GameState.GameOver;
    }
}
