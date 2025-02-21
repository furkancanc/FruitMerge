using System;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonsParent;
    [SerializeField] private GameObject purchaseButton;

    [Header("Data")]
    [SerializeField] private SkinDataSO[] skinDataSOs;
    private bool[] unlockedStates;
    private const string skinButtonKey = "SkinButton_";
    private const string lastSelectedSkinKey = "LastSelectedSkin";

    [Header("Variable")]
    private int lastSelectedSkin;

    [Header("Actions")]
    public static Action<SkinDataSO> onSkinSelected;

    private void Awake()
    {
        unlockedStates = new bool[skinDataSOs.Length];
    }

    private void Start()
    {
        Initialize();
        LoadData();
    }

    public void PurchaseButtonCallback()
    {
        // Check if we have enough coins

        // If that's the case, unlock the skin
        unlockedStates[lastSelectedSkin] = true;

        SaveData();

        // Calling the method to trigget the event & hide the purchase button
        SkinButtonClickedCallback(lastSelectedSkin);
    }

    private void Initialize()
    {
        purchaseButton.SetActive(false);

        for (int i = 0; i < skinDataSOs.Length; ++i)
        {
            SkinButton skinButtonInstance = Instantiate(skinButtonPrefab, skinButtonsParent);
            skinButtonInstance.Configure(skinDataSOs[i].GetObjectPrefabs()[0].GetSprite());


            int j = i;
            skinButtonInstance.GetButton().onClick.AddListener(() => SkinButtonClickedCallback(j));
        }
    }

    private void SkinButtonClickedCallback(int skinButtonIndex, bool shouldSaveLastSkin = true)
    {
        lastSelectedSkin = skinButtonIndex;

        for (int i = 0; i < skinButtonsParent.childCount; ++i)
        {
            SkinButton currentSkinButton = skinButtonsParent.GetChild(i).GetComponent<SkinButton>();

            if (i == skinButtonIndex)
            {
                currentSkinButton.Select();
            }
            else
            {
                currentSkinButton.Unselect();
            }
        }

        if (IsSkinUnlocked(skinButtonIndex))
        {
            onSkinSelected?.Invoke(skinDataSOs[skinButtonIndex]);

            if (shouldSaveLastSkin)
                SaveLastSelectedSkin();
        }

        ManagePurchaseButtonVisibility(skinButtonIndex);
    }

    private void ManagePurchaseButtonVisibility(int skinButtonIndex)
    {
        purchaseButton.SetActive(!IsSkinUnlocked(skinButtonIndex));
    }

    private bool IsSkinUnlocked(int skinButtonIndex)
    {
        return unlockedStates[skinButtonIndex];
    }

    private void LoadData()
    {
        for (int i = 0; i < unlockedStates.Length; ++i)
        {
            int unlockedValue = PlayerPrefs.GetInt(skinButtonKey + i);

            if (i == 0)
                unlockedValue = 1;

            unlockedStates[i] = unlockedValue == 1 ? true : false;
;        }
    }

    private void SaveData()
    {
        for (int i = 0; i < unlockedStates.Length; ++i)
        {
            int unlockedValue = unlockedStates[i] ? 1 : 0;
            PlayerPrefs.SetInt(skinButtonKey + i, unlockedValue);
        }
    }

    private void LoadLastSelectedSkin()
    {
        int lastSelectedSkinIndex = PlayerPrefs.GetInt(lastSelectedSkinKey);
        SkinButtonClickedCallback(lastSelectedSkinIndex, false);
    }

    private void SaveLastSelectedSkin()
    {
        PlayerPrefs.SetInt(lastSelectedSkinKey, lastSelectedSkin);
    }
}
