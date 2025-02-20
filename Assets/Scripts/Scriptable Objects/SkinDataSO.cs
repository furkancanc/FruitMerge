using UnityEngine;

[CreateAssetMenu(fileName = "Skin Data", menuName = "Scriptable Objects/Skin Data", order = 0)]
public class SkinDataSO : ScriptableObject
{
    [SerializeField] private Fruit[] objectPrefabs;
    [SerializeField] private Fruit[] spawnablePrefabs;
}
