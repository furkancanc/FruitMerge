using System;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header(" Data ")]
    [SerializeField] private FruitType fruitType;

    [Header(" Actions ")]
    public static Action<Fruit, Fruit> onCollisionWithFruit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePhysics()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Fruit otherFruit))
        {
            if (otherFruit.GetFruitType() != fruitType)
            {
                return;
            }

            onCollisionWithFruit?.Invoke(this, otherFruit);
        }
    }
    public FruitType GetFruitType()
    {
        return fruitType;
    }

}
