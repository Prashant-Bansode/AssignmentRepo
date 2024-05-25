using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton instance of UIManager
    public static UIManager Instance;

    // Reference to the UI Text component to display tile information
    public Text tileInfoText;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Ensure there is only one instance of UIManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }
    }

    // Method to display tile information on the UI
    public void DisplayTileInfo(int x, int y)
    {
        // Check if the tile info text component is assigned
        if (tileInfoText != null)
        {
            // Display tile information
            tileInfoText.text = $"Tile: ({x}, {y})";
            // Log the displayed tile information
            Debug.Log($"Displaying tile info: ({x}, {y})");
        }
        else
        {
            // Log an error if the tile info text component is not assigned
            Debug.LogError("Tile Info Text is not assigned in the UIManager.");
        }
    }
}
