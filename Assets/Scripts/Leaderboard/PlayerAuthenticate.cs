using UnityEngine;
using LootLocker.Requests;
using System.Collections;
public class PlayerAuthenticate : MonoBehaviour
{
    public string PlayerId { get; private set; }

    void Start()
    {
        StartCoroutine(LoginCoroutine());
    }

    IEnumerator LoginCoroutine()
    {
        bool done = false;

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if(response.success)
            {
                Debug.Log("Connected !");
                PlayerId = response.player_id.ToString();

                done = true;
            }
            else
            {
                Debug.Log("Error connecting the player...");
                done = true;
            }
        });

        yield return new WaitWhile(() => done == true);
    }

    public string GetPlayerId()
    {
        return playerId;
    }
}
