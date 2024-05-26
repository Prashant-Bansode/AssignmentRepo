// EnemyAI.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3.0f;
    private Pathfinding pathfinding;
    private bool isMoving = false;
    private float updatePathInterval = 0.5f;
    private float pathUpdateTimer;
    private List<EnemyAI> allEnemies;

    private void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        pathUpdateTimer = updatePathInterval;
        allEnemies = new List<EnemyAI>(FindObjectsOfType<EnemyAI>());
    }

    private void Update()
    {
        pathUpdateTimer -= Time.deltaTime;
        if (pathUpdateTimer <= 0)
        {
            pathUpdateTimer = updatePathInterval;
            if (!isMoving)
            {
                Vector3 targetPosition = GetAdjacentPlayerPosition();
                if (targetPosition != Vector3.zero && Vector3.Distance(transform.position, targetPosition) > 0.5f)
                {
                    MoveTowards(targetPosition);
                }
            }
        }
    }

    private Vector3 GetAdjacentPlayerPosition()
    {
        Vector3 playerPosition = new Vector3(Mathf.RoundToInt(player.position.x), 0, Mathf.RoundToInt(player.position.z));
        List<Vector3> adjacentPositions = new List<Vector3>
        {
            playerPosition + Vector3.forward,
            playerPosition + Vector3.back,
            playerPosition + Vector3.left,
            playerPosition + Vector3.right
        };

        Vector3 bestPosition = Vector3.zero;
        float shortestDistance = float.MaxValue;

        foreach (var pos in adjacentPositions)
        {
            if (IsPositionWalkable(pos) && pos != playerPosition)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, pos);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    bestPosition = pos;
                }
            }
        }

        return bestPosition;
    }

    private bool IsPositionWalkable(Vector3 position)
    {
        if (!pathfinding.IsWithinBounds(position)) return false;
        Node node = pathfinding.nodes[(int)position.x, (int)position.z];
        return node.isWalkable && !IsPlayerOrEnemyPosition(position);
    }

    private bool IsPlayerOrEnemyPosition(Vector3 position)
    {
        if (Vector3.Distance(player.position, position) < 0.5f)
        {
            return true;
        }

        foreach (var enemy in allEnemies)
        {
            if (enemy != this && Vector3.Distance(enemy.transform.position, position) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveTowardsCoroutine(targetPosition));
        }
    }

    private IEnumerator MoveTowardsCoroutine(Vector3 targetPosition)
    {
        isMoving = true;

        List<Vector3> enemyPositions = GetEnemyPositions();
        List<Vector3> path = pathfinding.FindPath(transform.position, targetPosition, player.position, enemyPositions);
        if (path != null)
        {
            foreach (var step in path)
            {
                while (Vector3.Distance(transform.position, step) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, step, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }

        isMoving = false;
    }

    private List<Vector3> GetEnemyPositions()
    {
        List<Vector3> enemyPositions = new List<Vector3>();
        foreach (var enemy in allEnemies)
        {
            if (enemy != this)
            {
                Vector3 enemyPosition = new Vector3(Mathf.RoundToInt(enemy.transform.position.x), 0, Mathf.RoundToInt(enemy.transform.position.z));
                enemyPositions.Add(enemyPosition);
            }
        }
        return enemyPositions;
    }
}


