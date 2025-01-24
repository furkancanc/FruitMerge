using System;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private void Start()
    {
        Fruit.onCollisionWithFruit += CollisionBetweenFruitsCallback;
    }

    private void OnDestroy()
    {
        Fruit.onCollisionWithFruit -= CollisionBetweenFruitsCallback;
    }

    private void CollisionBetweenFruitsCallback(Fruit sender)
    {
        
    }
}
