using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class FruitManager : MonoBehaviour
{
    public static FruitManager instance;

    [Header(" Elements ")]
    [SerializeField] private SkinDataSO skinData;
    [SerializeField] private Transform fruitsParent;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;

    [Header(" Settings ")]
    [SerializeField] private float fruitsYSpawnPosition;
    [SerializeField] private float spawnDelay;
    private bool canControl;
    private bool isControlling;

    [Header(" Next Fruit Settings ")]
    private int nextFruitIndex;

    [Header(" Debug ")]
    [SerializeField] private bool enableGizmos;

    [Header(" Actions ")]
    public static Action onNextFruitIndexSet;
    public static Action<Fruit> onFruitSpawned;

    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessedCallback;
        ShopManager.onSkinSelected += SkinSelectedCallback;

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCallback;
        ShopManager.onSkinSelected -= SkinSelectedCallback;
    }

    private void Start()
    {
        SetNextFruitIndex();

        canControl = true;
        HideLine();
    }

    private void Update()
    {
        if (!GameManager.instance.IsGameState())
        {
            return;
        }

        if (canControl)
        {
            ManagePlayerInput();
        }
    }

    private void SkinSelectedCallback(SkinDataSO skinDataSO)
    {
        skinData = skinDataSO;
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
        if (!IsClickDetected())
            return;

        DisplayLine();
        PlaceLineAtClickedPosition();

        SpawnFruit();

        isControlling = true;
    }

    private bool IsClickDetected()
    {
        Vector2 mousePos = Input.mousePosition;
        return mousePos.y > Screen.height / 4;
    }

    private void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();

        currentFruit.MoveTo(new Vector2(GetSpawnPosition().x, fruitsYSpawnPosition));
    }

    private void MouseUpCallback()
    {
        HideLine();

        if (currentFruit != null)
        {
            currentFruit.EnablePhysics();
        }
        
        canControl = false;
        isControlling = false;
        StartControlTimer();
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        Fruit fruitToInstantiate = skinData.GetSpawnablePrefabs()[nextFruitIndex];

        currentFruit = Instantiate(fruitToInstantiate, spawnPosition, Quaternion.identity, fruitsParent);
        SetNextFruitIndex();

        onFruitSpawned?.Invoke(currentFruit);
    }

    private void SetNextFruitIndex()
    {
        nextFruitIndex = Random.Range(0, skinData.GetSpawnablePrefabs().Length);
        onNextFruitIndexSet?.Invoke();
    }

    public string GetNextFruitName()
    {
        return skinData.GetSpawnablePrefabs()[nextFruitIndex].name;
    }

    public Sprite GetNextFruitSprite()
    {
        return skinData.GetSpawnablePrefabs()[nextFruitIndex].GetSprite();
    }

    public Fruit[] GetSmallFruits()
    {
        List<Fruit> smallFruits = new List<Fruit>();

        for (int i = 0; i < fruitsParent.childCount; ++i)
        {
            Fruit fruit = fruitsParent.GetChild(i).GetComponent<Fruit>();

            int fruitTypeInt = (int)fruit.GetFruitType(); ;
            if (fruitTypeInt < 3)
                smallFruits.Add(fruit);
        }

        return smallFruits.ToArray();
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
        Invoke("StopControlTimer", spawnDelay);
    }

    private void StopControlTimer()
    {
        canControl = true;
    }

    private void MergeProcessedCallback(FruitType fruitType, Vector2 spawnPosition)
    {
        for (int i = 0; i < skinData.GetObjectPrefabs().Length; ++i)
        {
            if (skinData.GetObjectPrefabs()[i].GetFruitType() == fruitType)
            {
                SpawnMergedFruit(skinData.GetObjectPrefabs()[i], spawnPosition);
                break;
            }
        }
    }

    private void SpawnMergedFruit(Fruit fruit, Vector2 spawnPosition)
    {
        Fruit fruitInstance = Instantiate(fruit, spawnPosition, Quaternion.identity, fruitsParent);
        fruitInstance.EnablePhysics();
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
