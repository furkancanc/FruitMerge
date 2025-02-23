using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Scriptable Objects/Level Data", order = 1)]
public class LevelDataSO : ScriptableObject
{
    [Header("Data")]
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private int requiredHighScore;

    public GameObject GetLevel() => levelPrefab;
    public int GetRequiredHighscore() => requiredHighScore;
}
