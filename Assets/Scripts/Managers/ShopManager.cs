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

    [Header("Actions")]
    public static Action<SkinDataSO> onSkinSelected;

    private void Awake()
    {
        unlockedStates = new bool[skinDataSOs.Length];
        LoadData();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        purchaseButton.SetActive(false);

        for (int i = 0; i < skinDataSOs.Length; ++i)
        {
            SkinButton skinButtonInstance = Instantiate(skinButtonPrefab, skinButtonsParent);
            skinButtonInstance.Configure(skinDataSOs[i].GetObjectPrefabs()[0].GetSprite());

            if (i == 0)
                skinButtonInstance.Select();

            int j = i;
            skinButtonInstance.GetButton().onClick.AddListener(() => SkinButtonClickedCallback(j));
        }
    }

    private void SkinButtonClickedCallback(int skinButtonIndex)
    {
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
            int unlockedValue = PlayerPrefs.GetInt("SkinButton_" + i);

            if (i == 0)
                unlockedValue = 1;

            unlockedStates[i] = unlockedValue == 1 ? true : false;
;        }
    }
}
