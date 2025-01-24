using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private GameObject currentFruit;

    [Header(" Settings ")]
    [SerializeField] private float fruitsYSpawnPosition;

    [Header(" Debug ")]
    [SerializeField] private bool enableGizmos;
    
    private void Update()
    {
        ManagePlayerInput();
    }

    private void Start()
    {
        HideLine();
    }

    private void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownCallback();
        }
        else if (Input.GetMouseButton(0))
        {
            MouseDragCallback();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MouseUpCallback();
        }
    }

    private void MouseDownCallback()
    {
        DisplayLine();
        PlaceLineAtClickedPosition();

        SpawnFruit();
    }

    private void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();

        currentFruit.transform.position = new Vector2(GetSpawnPosition().x, fruitsYSpawnPosition);
    }

    private void MouseUpCallback()
    {
        HideLine();
        currentFruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        currentFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetClickedWorldPosition()
    {
        if (!(Input.mousePosition.x < 0 || Input.mousePosition.x >= Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y >= Screen.height))
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        return Vector2.up;
    }
    private Vector2 GetSpawnPosition()
    {
        Vector2 worldClickedPosition = GetClickedWorldPosition();
        worldClickedPosition.y = fruitsYSpawnPosition;
        return worldClickedPosition;
    }

    private void PlaceLineAtClickedPosition()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }

    private void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }

    private void DisplayLine()
    {
        fruitSpawnLine.enabled = true;
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
