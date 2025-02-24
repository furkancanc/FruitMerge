using UnityEngine;
using LootLocker.Requests;
using System.Collections;
public class PlayerAuthenticate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}
