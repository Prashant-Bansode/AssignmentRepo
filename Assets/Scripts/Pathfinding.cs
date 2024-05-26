// Pathfinding.cs
using UnityEngine;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour
{
    public GridManager gridManager;
    public ObstacleData obstacleData;
    private List<Node> openList;
    private List<Node> closedList;
    public Node[,] nodes;

    private void Awake()
    {
        InitializeNodes();
    }

    private void InitializeNodes()
    {
        nodes = new Node[gridManager.gridSize, gridManager.gridSize];
        for (int x = 0; x < gridManager.gridSize; x++)
        {
            for (int y = 0; y < gridManager.gridSize; y++)
            {
                nodes[x, y] = new Node(x, y, !obstacleData.obstacleGrid[x * gridManager.gridSize + y]);
            }
        }
    }

    public List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos, Vector3 playerPos, List<Vector3> enemyPositions)
    {
        Node startNode = nodes[Mathf.RoundToInt(startPos.x), Mathf.RoundToInt(startPos.z)];
        Node targetNode = nodes[Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.z)];

        openList = new List<Node> { startNode };
        closedList = new List<Node>();

        foreach (Node node in nodes)
        {
            node.gCost = int.MaxValue;
            node.CalculateFCost();
            node.cameFromNode = null;
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, targetNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openList);

            if (currentNode == targetNode)
            {
                return CalculatePath(targetNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbor in GetNeighborList(currentNode))
            {
                if (closedList.Contains(neighbor)) continue;
                if (!neighbor.isWalkable || IsOccupied(neighbor, playerPos, enemyPositions))
                {
                    closedList.Add(neighbor);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbor);

                if (tentativeGCost < neighbor.gCost)
                {
                    neighbor.cameFromNode = currentNode;
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = CalculateDistanceCost(neighbor, targetNode);
                    neighbor.CalculateFCost();

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private List<Vector3> CalculatePath(Node endNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(new Vector3(currentNode.x, 0, currentNode.y));
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        return xDistance + yDistance;
    }

    private Node GetLowestFCostNode(List<Node> nodeList)
    {
        Node lowestFCostNode = nodeList[0];
        for (int i = 1; i < nodeList.Count; i++)
        {
            if (nodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private List<Node> GetNeighborList(Node currentNode)
    {
        List<Node> neighborList = new List<Node>();

        if (currentNode.x - 1 >= 0) neighborList.Add(nodes[currentNode.x - 1, currentNode.y]);
        if (currentNode.x + 1 < gridManager.gridSize) neighborList.Add(nodes[currentNode.x + 1, currentNode.y]);
        if (currentNode.y - 1 >= 0) neighborList.Add(nodes[currentNode.x, currentNode.y - 1]);
        if (currentNode.y + 1 < gridManager.gridSize) neighborList.Add(nodes[currentNode.x, currentNode.y + 1]);

        return neighborList;
    }

    public bool IsWithinBounds(Vector3 position)
    {
        return position.x >= 0 && position.x < gridManager.gridSize && position.z >= 0 && position.z < gridManager.gridSize;
    }

    private bool IsOccupied(Node node, Vector3 playerPos, List<Vector3> enemyPositions)
    {
        Vector3 nodePosition = new Vector3(node.x, 0, node.y);
        if (Vector3.Distance(nodePosition, playerPos) < 0.5f)
        {
            return true;
        }
        foreach (var enemyPos in enemyPositions)
        {
            if (Vector3.Distance(nodePosition, enemyPos) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }
}
