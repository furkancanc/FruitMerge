using TMPro;
using UnityEngine;

[RequireComponent(typeof(FruitManager))]
public class FruitManagerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI nextFruitText;
    private FruitManager fruitManager;

    private void Start()
    {
        fruitManager = GetComponent<FruitManager>();
    }

    private void Update()
    {
        nextFruitText.text = fruitManager.GetNextFruitName();
    }
}
