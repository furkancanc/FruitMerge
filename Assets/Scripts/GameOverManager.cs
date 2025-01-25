using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject deadLine;
    [SerializeField] private Transform fruitsParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        for (int i = 0; i < fruitsParent.childCount; ++i)
        {
            Fruit fruit = fruitsParent.GetChild(i).GetComponent<Fruit>();

            if (!fruit.HasCollided())
            {
                continue;
            }

            CheckIfFruitAboveLine(fruitsParent.GetChild(i));
        }
    }

    private void CheckIfFruitAboveLine(Transform fruit)
    {
        if (fruit.position.y > deadLine.transform.position.y)
        {
            Debug.Log("GameOver");
        }
    }
}
