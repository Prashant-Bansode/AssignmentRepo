using UnityEngine;

public class Tile : MonoBehaviour
{
    // Coordinates of the tile in the grid
    public int x, y;

    // Method to initialize the tile coordinates
    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // Called when the mouse hovers over the tile
    private void OnMouseOver()
    {
        // Check if UIManager instance exists
        if (UIManager.Instance != null)
        {
            // Call UIManager to display tile information
            UIManager.Instance.DisplayTileInfo(x, y);
            // Log the tile coordinates to the console
            Debug.Log($"Mouse over tile: ({x}, {y})");
        }
        else
        {
            // Log an error if UIManager instance is null
            Debug.LogError("UIManager instance is null.");
        }
    }
}
