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
        }
    }
}
