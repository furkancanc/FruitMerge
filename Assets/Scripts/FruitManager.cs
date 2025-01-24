using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject fruitPrefab;

    [Header(" Settings ")]
    [SerializeField] private float fruitsYSpawnPosition;

    [Header(" Debug ")]
    [SerializeField] private bool enableGizmos;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManagePlayerInput();
        }
    }

    private void ManagePlayerInput()
    {
        Vector2 spawnPosition = GetClickedWorldPosition();
        spawnPosition.y = fruitsYSpawnPosition;

        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-50, fruitsYSpawnPosition, 0), new Vector3(50, fruitsYSpawnPosition, 0));
    }
#endif
}
