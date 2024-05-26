using UnityEngine;

public interface AI
{
    void MoveTowards(Vector3 targetPosition);
}


//path = pathfinding.FindPath(transform.position, targetPosition, transform.position, GetEnemyPositions());
//List<Vector3> path = pathfinding.FindPath(transform.position, targetPosition, player.position, enemyPositions);