using UnityEngine;

public class LevelMapManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform mapContent;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        mapContent.anchoredPosition = Vector2.up * 1920 * (mapContent.childCount - 1);
    }
}
