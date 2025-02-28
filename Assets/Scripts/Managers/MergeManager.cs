using System;
using System.Collections;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    [Header(" Actions ")]
    public static Action<FruitType, Vector2> onMergeProcessed;

    [Header(" Settings ")]
    Fruit lastSender;

    private void Start()
    {
        Fruit.onCollisionWithFruit += CollisionBetweenFruitsCallback;
    }

    private void OnDestroy()
    {
        Fruit.onCollisionWithFruit -= CollisionBetweenFruitsCallback;
    }

    private void CollisionBetweenFruitsCallback(Fruit sender, Fruit otherFruit)
    {
        if (lastSender != null)
        {
            return;
        }

        lastSender = sender;

        ProcessMerge(sender, otherFruit);
    }

    private void ProcessMerge(Fruit sender, Fruit otherFruit)
    {
        FruitType mergeFruitType = sender.GetFruitType();
        mergeFruitType += 1;

        Vector2 fruitSpawnPos = (sender.transform.position + otherFruit.transform.position) / 2;

        sender.Merge();
        otherFruit.Merge();

        StartCoroutine(ResetLastSenderCoroutine());

        onMergeProcessed?.Invoke(mergeFruitType, fruitSpawnPos);
    }

    IEnumerator ResetLastSenderCoroutine()
    {
        yield return new WaitForEndOfFrame();
        lastSender = null;
    }
}
