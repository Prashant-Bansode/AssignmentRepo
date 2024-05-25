using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Pathfinding pathfinding;
    private List<Vector3> path; // Store the path as a list of Vector3 positions

    private void Update()
    {
        // Check for left mouse button click and not currently moving
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Get the target position clicked by the player
            Vector3 targetPosition = GetMouseWorldPosition();
            // Check if the target position is walkable
            if (IsPositionWalkable(targetPosition))
            {
                // Find a path from current position to target position
                path = pathfinding.FindPath(transform.position, targetPosition);
                // If a valid path is found, start moving along it
                if (path != null)
                {
                    StartCoroutine(MoveAlongPath(path));
                }
            }
        }
    }

    // Get the world position of the mouse cursor
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    // Check if a position is walkable
    private bool IsPositionWalkable(Vector3 position)
    {
        Node node = pathfinding.nodes[(int)position.x, (int)position.z];
        return node.isWalkable;
    }

    // Flag to indicate if the player is currently moving
    private bool isMoving = false;
    // Coroutine to move the player along the path
    private IEnumerator MoveAlongPath(List<Vector3> path)
    {
        // Set moving flag to true
        isMoving = true;
        // Loop through each position in the path
        foreach (Vector3 position in path)
        {
            // Move towards the current position in the path
            while (Vector3.Distance(transform.position, position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * 5);
                yield return null;
            }
        }
        // Set moving flag to false when movement is complete
        isMoving = false;
    }
}
