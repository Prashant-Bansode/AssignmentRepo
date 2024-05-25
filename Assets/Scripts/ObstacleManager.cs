using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // Reference to the scriptable object containing obstacle data
    public ObstacleData obstacleData;
    
    // Prefab of the obstacle to be instantiated
    public GameObject obstaclePrefab;

    // Called when the script is initialized
    private void Start()
    {
        // Generate obstacles upon start
        GenerateObstacles();
    }

    // Method to generate obstacles based on obstacle data
    private void GenerateObstacles()
    {
        // Loop through rows
        for (int i = 0; i < 10; i++)
        {
            // Loop through columns
            for (int j = 0; j < 10; j++)
            {
                // Calculate the index in the obstacleGrid array
                int index = i * 10 + j;
                
                // If the obstacleGrid cell at index is true, instantiate an obstacle
                if (obstacleData.obstacleGrid[index])
                {
                    // Instantiate obstaclePrefab at the corresponding position in the grid
                    Instantiate(obstaclePrefab, new Vector3(i, 0.5f, j), Quaternion.identity);
                }
            }
        }
    }
}
