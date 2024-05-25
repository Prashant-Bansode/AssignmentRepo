using UnityEngine;

// This class handles raycasting from the mouse position to detect tiles in a specified layer.
public class MouseRaycaster : MonoBehaviour
{
    // The layer mask that specifies which layer the tiles are on.
    public LayerMask tileLayerMask;

    // Update is called once per frame.
    void Update()
    {
        // Check if the left mouse button is pressed.
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera through the mouse position.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform a raycast and check if it hits an object in the tile layer.
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayerMask))
            {
                // Try to get the Tile component from the hit object.
                Tile tile = hit.collider.GetComponent<Tile>();

                // If the hit object has a Tile component, log its coordinates.
                if (tile != null)
                {
                    Debug.Log($"Clicked on tile: ({tile.x}, {tile.y})");
                }
            }
        }
    }
}
