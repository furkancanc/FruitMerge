using System;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header(" Actions ")]
    public static Action<Fruit> onCollisionWithFruit;

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
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Fruit fruit))
        {
            onCollisionWithFruit?.Invoke(this);
        }
    }
}
