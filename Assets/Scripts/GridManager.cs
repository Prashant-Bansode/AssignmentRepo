using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Prefab for the grid tiles
    public GameObject tilePrefab;
    // Size of the grid (gridSize x gridSize)
    public int gridSize = 10;

    // Initialization
    private void Start()
    {
        // Generate the grid when the game starts
        GenerateGrid();
    }

    // Method to generate the grid
    private void GenerateGrid()
    {
        // Loop through each position in the grid
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Instantiate a tile at the current position
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                // Initialize the tile with its grid coordinates
                tile.GetComponent<Tile>().Initialize(x, y);
            }
        }
    }
}
