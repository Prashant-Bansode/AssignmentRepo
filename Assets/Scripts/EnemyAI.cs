using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour, AI
{
    // Reference to the player Transform
    public Transform player;
    // Movement speed of the enemy
    public float moveSpeed = 3.0f;
    // Reference to the Pathfinding component
    private Pathfinding pathfinding;
    // Flag to check if the enemy is currently moving
    private bool isMoving = false;

    // Initialization
    private void Start()
    {
        // Find the Pathfinding component in the scene
        pathfinding = FindObjectOfType<Pathfinding>();
    }

    // Method to move the enemy towards the target position
    public void MoveTowards(Vector3 targetPosition)
    {
        // Start the movement coroutine if not already moving
        if (!isMoving)
        {
            StartCoroutine(MoveTowardsCoroutine(targetPosition));
        }
    }

    // Coroutine to move the enemy along the path towards the target position
    private IEnumerator MoveTowardsCoroutine(Vector3 targetPosition)
    {
        isMoving = true;

        // Find the path to the target position
        List<Vector3> path = pathfinding.FindPath(transform.position, targetPosition);
        if (path != null)
        {
            // Move along each step in the path
            foreach (var step in path)
            {
                // Move towards the current step until close enough
                while (Vector3.Distance(transform.position, step) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, step, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }

        isMoving = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the enemy is not moving and is far from the player
        if (!isMoving && Vector3.Distance(transform.position, player.position) > 1.5f)
        {
            // Get the adjacent tile to move towards
            Vector3 targetPosition = GetAdjacentTile(player.position);
            MoveTowards(targetPosition);
        }
    }

    // Method to get an adjacent tile next to the player
    private Vector3 GetAdjacentTile(Vector3 playerPosition)
    {
        // Possible directions to check for adjacent tiles
        Vector3[] directions = {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1)
        };

        // Check each direction for a valid adjacent tile
        foreach (Vector3 direction in directions)
        {
            Vector3 adjacentTile = playerPosition + direction;
            if (pathfinding.IsWithinBounds(adjacentTile) && !pathfinding.IsObstacle(new Node((int)adjacentTile.x, (int)adjacentTile.z, true)))
            {
                return adjacentTile;
            }
        }

        // Return the player's position if no valid adjacent tile is found
        return playerPosition;
    }
}
