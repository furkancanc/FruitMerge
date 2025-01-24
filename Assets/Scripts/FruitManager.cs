using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Fruit fruitPrefab;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;

    [Header(" Settings ")]
    [SerializeField] private float fruitsYSpawnPosition;
    private bool canControl;
    private bool isControlling;

    [Header(" Debug ")]
    [SerializeField] private bool enableGizmos;
    
    private void Update()
    {
        if (canControl)
        {
            ManagePlayerInput();
        }
    }

    private void Start()
    {
        canControl = true;
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
            if (isControlling)
            {
                MouseDragCallback();
            }
            else
            {
                MouseDownCallback();
            }
            
        }
        else if (Input.GetMouseButtonUp(0) && isControlling)
        {
            MouseUpCallback();
        }
    }

    private void MouseDownCallback()
    {
        DisplayLine();
        PlaceLineAtClickedPosition();

        SpawnFruit();

        isControlling = true;
    }

    private void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();

        currentFruit.MoveTo(new Vector2(GetSpawnPosition().x, fruitsYSpawnPosition));
    }

    private void MouseUpCallback()
    {
        HideLine();
        currentFruit.EnablePhysics();

        canControl = false;
        isControlling = false;
        StartControlTimer();
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

    private void StartControlTimer()
    {
        Invoke("StopControlTimer", 1);
    }

    private void StopControlTimer()
    {
        canControl = true;
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
