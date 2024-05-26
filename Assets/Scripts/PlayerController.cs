// PlayerController.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Pathfinding pathfinding;
    public List<EnemyAI> enemies; // List of enemy units
    private List<Vector3> path;
    private bool isMoving = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Vector3 targetPosition = GetMouseWorldPosition();
            if (IsPositionWalkable(targetPosition))
            {
                path = pathfinding.FindPath(transform.position, targetPosition, transform.position, GetEnemyPositions());
                if (path != null)
                {
                    StartCoroutine(MoveAlongPath(path));
                }
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return new Vector3(Mathf.RoundToInt(hit.point.x), 0, Mathf.RoundToInt(hit.point.z));
        }
        return Vector3.zero;
    }

    private bool IsPositionWalkable(Vector3 position)
    {
        if (!pathfinding.IsWithinBounds(position)) return false;
        Node node = pathfinding.nodes[(int)position.x, (int)position.z];
        return node.isWalkable && !IsEnemyPosition(position);
    }

    private bool IsEnemyPosition(Vector3 position)
    {
        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, position) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator MoveAlongPath(List<Vector3> path)
    {
        isMoving = true;
        foreach (Vector3 position in path)
        {
            while (Vector3.Distance(transform.position, position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * 5);
                yield return null;
            }
        }
        isMoving = false;
    }

    private List<Vector3> GetEnemyPositions()
    {
        List<Vector3> enemyPositions = new List<Vector3>();
        foreach (var enemy in enemies)
        {
            Vector3 enemyPosition = new Vector3(Mathf.RoundToInt(enemy.transform.position.x), 0, Mathf.RoundToInt(enemy.transform.position.z));
            enemyPositions.Add(enemyPosition);
        }
        return enemyPositions;
    }
}