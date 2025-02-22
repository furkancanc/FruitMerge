using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlastButtonCallback()
    {
        Debug.Log("Blast!!!");

        Fruit[] smallFruits = FruitManager.instance.GetSmallFruits();

        foreach (Fruit smallFruit in smallFruits)
        {
            smallFruit.Merge();
        }
    }
}
