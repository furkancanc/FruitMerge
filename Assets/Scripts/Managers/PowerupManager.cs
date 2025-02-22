using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Button blastButton;

    [Header("Settings")]
    [SerializeField] private int blastPrice;

    private void Awake()
    {
        CoinManager.onCoinsUpdated += CoinsUpadtedCallback;
    }

    private void OnDestroy()
    {
        CoinManager.onCoinsUpdated -= CoinsUpadtedCallback;
    }
    private void CoinsUpadtedCallback()
    {
        ManageBlastButtonInteractability();
    }

    public void BlastButtonCallback()
    {
        Fruit[] smallFruits = FruitManager.instance.GetSmallFruits();

        if (smallFruits.Length <= 0)
            return;

        foreach (Fruit smallFruit in smallFruits)
        {
            smallFruit.Merge();
        }

        CoinManager.instance.AddCoins(-blastPrice);
    }

    private void ManageBlastButtonInteractability()
    {
        bool canBlast = CoinManager.instance.CanPurchase(blastPrice);
        blastButton.interactable = canBlast;
    }
}
