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

    }

    private void LoadData() => coins = PlayerPrefs.GetInt(coinsKey);
    private void SaveData() => PlayerPrefs.SetInt(coinsKey, coins);
}
