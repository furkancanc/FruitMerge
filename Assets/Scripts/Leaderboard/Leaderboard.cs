using System.Collections;
using UnityEngine;
using LootLocker.Requests;

public class Leaderboard : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string leaderboardKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SubmitScoresCoroutine(string memberId, int score)
    {
        LootLockerSDKManager.SubmitScore(memberId, score, leaderboardKey, (response) =>
        {

        });

        yield return null;
    }
}
