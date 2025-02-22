using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Variables")]
    private int coins;
    private const string coinsKey = "coins";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadData();
        UpdateCoinTexts();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        coins = Mathf.Max(0, coins);

        SaveData();

        UpdateCoinTexts();
    }

    public bool CanPurchase(int price)
    {
        return coins >= price;
    }

    private void UpdateCoinTexts()
    {
        GameObject[] coinTextGO = GameObject.FindGameObjectsWithTag("CoinText");

        for (int i = 0; i < coinTextGO.Length; i++)
        {
            coinTextGO[i].GetComponent<TextMeshProUGUI>().text = coins.ToString();
        }

    }

    private void LoadData() => coins = PlayerPrefs.GetInt(coinsKey);
    private void SaveData() => PlayerPrefs.SetInt(coinsKey, coins);
}
