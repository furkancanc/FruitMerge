using LootLocker.Requests;
using UnityEngine;

public class PlayerLeaderboard : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerAuthenticate playerAuthenticate;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetRandomScore();
        }
    }

    private void SetRandomScore()
    {
        int score = Random.Range(0, 10000);
        string playerId = playerAuthenticate.PlayerId;

        Leaderboard.instance.SubmitScore(playerId, score);

    }

    public void SetPlayerName(string playerName)
    {
        LootLockerSDKManager.SetPlayerName(playerName, (response) => 
        {
            if (response.success)
            {
                Debug.Log("Player name has been set : " + playerName);
            }
            else
            {
                Debug.Log("Error setting the player name...");
            }
        });
    }
}
