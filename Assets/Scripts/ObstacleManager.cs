// ObstacleManager.cs
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    private void Start()
    {
        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int index = i * 10 + j;
                if (obstacleData.obstacleGrid[index])
                {
                    Instantiate(obstaclePrefab, new Vector3(i, 0.5f, j), Quaternion.identity);
                }
            }
        }
    }
}
