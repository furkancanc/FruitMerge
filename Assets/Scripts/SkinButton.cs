using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectionOutline;

    public void Configure(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }

    public void Select() => selectionOutline.SetActive(true);
    public void Unselect() => selectionOutline.SetActive(false);
}
