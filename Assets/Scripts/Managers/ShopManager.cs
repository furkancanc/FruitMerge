using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonsParent;

    [Header("Data")]
    [SerializeField] private SkinDataSO[] skinDataSOs;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
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
    }
}
