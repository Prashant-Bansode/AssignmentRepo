using UnityEngine;

// This attribute allows creating instances of this class via Unity's editor
[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    // Array to represent the obstacle grid
    public bool[] obstacleGrid = new bool[100];
}
