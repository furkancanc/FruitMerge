using System.Collections;
using UnityEngine;
using LootLocker.Requests;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;

    [Header("Settings")]
    [SerializeField] private string leaderboardKey;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitScore(string memberId, int score)
    {
        StartCoroutine(SubmitScoreCoroutine(memberId, score));
    }

    IEnumerator SubmitScoreCoroutine(string memberId, int score)
    {
        bool done = false;

        LootLockerSDKManager.SubmitScore(memberId, score, leaderboardKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score sent : " + score);
                done = true;
            }
            else
            {
                Debug.Log("Score not sent...");
                done = true;
            }
        });

        yield return null;
    }
}
