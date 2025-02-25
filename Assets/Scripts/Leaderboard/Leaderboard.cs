using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using System;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI leaderboardText;

    [Header("Settings")]
    [SerializeField] private string leaderboardKey;

    public static Action<LootLockerLeaderboardMember[]> onLeaderboardFetched; 
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

    [NaughtyAttributes.Button]
    private void FetchScores()
    {
        StartCoroutine(FetchScoresCoroutine());
    }

    IEnumerator FetchScoresCoroutine()
    {
        bool done = false;

        LootLockerSDKManager.GetScoreList(leaderboardKey, 10, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;

                onLeaderboardFetched?.Invoke(members);

                //leaderboardText.text = "Names - Scores\n";

                //for (int i = 0; i < members.Length; ++i)
                //{
                //    string playerName = GetPlayerName(members[i]);
                //    leaderboardText.text += playerName + " - " + members[i].score + "\n";
                //}

                done = true;
            }
            else
            {
                Debug.Log("Failed to fetch leaderboard...");
                done = true;
            }
        });

        yield return new WaitWhile(() => done == true);
    }

    private string GetPlayerName(LootLockerLeaderboardMember member)
    {
        string playerName = member.member_id;

        if (member.player.name.Length > 0)
        {
            playerName = member.player.name;
        }

        return playerName;
    }
}
