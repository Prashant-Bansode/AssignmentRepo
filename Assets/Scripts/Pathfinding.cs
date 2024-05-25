using UnityEngine;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    // Reference to the grid manager
    public GridManager gridManager;
    
    // Reference to obstacle data
    public ObstacleData obstacleData;

    // Lists to store nodes during pathfinding
    private List<Node> openList;
    private List<Node> closedList;

    // 2D array to store nodes
    public Node[,] nodes;

    private void Awake()
    {
        // Initialize nodes when the script is initialized
        InitializeNodes();
    }

    private void InitializeNodes()
    {
        // Create a 2D array to hold nodes
        nodes = new Node[gridManager.gridSize, gridManager.gridSize];

        // Loop through grid coordinates and initialize nodes
        for (int x = 0; x < gridManager.gridSize; x++)
        {
            for (int y = 0; y < gridManager.gridSize; y++)
            {
                // Create a node with walkable status based on obstacle data
                nodes[x, y] = new Node(x, y, !obstacleData.obstacleGrid[x * gridManager.gridSize + y]);
            }
        }
    }

    // Find a path from start position to target position
    public List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // Get start and target nodes
        Node startNode = nodes[(int)startPos.x, (int)startPos.z];
        Node targetNode = nodes[(int)targetPos.x, (int)targetPos.z];

        // Initialize open and closed lists
        openList = new List<Node> { startNode };
        closedList = new List<Node>();

        // Reset node costs and references
        foreach (Node node in nodes)
        {
            node.gCost = int.MaxValue;
            node.CalculateFCost();
            node.cameFromNode = null;
        }

        // Set start node costs
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, targetNode);
        startNode.CalculateFCost();

        // Loop until open list is empty
        while (openList.Count > 0)
        {
            // Get node with lowest fCost from open list
            Node currentNode = GetLowestFCostNode(openList);
            
            // If current node is the target node, return the path
            if (currentNode == targetNode)
            {
                return CalculatePath(targetNode);
            }

            // Move current node from open list to closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // Explore neighbors of current node
            foreach (Node neighbor in GetNeighborList(currentNode))
            {
                // Skip if neighbor is in closed list or is unwalkable
                if (closedList.Contains(neighbor)) continue;
                if (!neighbor.isWalkable)
                {
                    closedList.Add(neighbor);
                    continue;
                }

                // Calculate tentative gCost for neighbor
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbor);
                
                // If tentative gCost is lower than current gCost, update neighbor
                if (tentativeGCost < neighbor.gCost)
                {
                    neighbor.cameFromNode = currentNode;
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = CalculateDistanceCost(neighbor, targetNode);
                    neighbor.CalculateFCost();

                    // Add neighbor to open list if not already in it
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        // If no path found, return null
        return null;
    }

    // Calculate path from end node to start node
    private List<Vector3> CalculatePath(Node endNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    // Calculate distance cost between two nodes
    private int CalculateDistanceCost(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        return xDistance + yDistance;
    }

    // Get node with lowest fCost from a list of nodes
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

    // Get list of neighboring nodes for a given node
    private List<Node> GetNeighborList(Node currentNode)
    {
        List<Node> neighborList = new List<Node>();

        // Check neighbors in all four directions
        if (currentNode.x - 1 >= 0) neighborList.Add(nodes[currentNode.x - 1, currentNode.y]);
        if (currentNode.x + 1 < gridManager.gridSize) neighborList.Add(nodes[currentNode.x + 1, currentNode.y]);
        if (currentNode.y - 1 >= 0) neighborList.Add(nodes[currentNode.x, currentNode.y - 1]);
        if (currentNode.y + 1 < gridManager.gridSize) neighborList.Add(nodes[currentNode.x, currentNode.y + 1]);

        return neighborList;
    }

    // Check if a position is within the grid bounds
    public bool IsWithinBounds(Vector3 position)
    {
        return position.x >= 0 && position.x < gridManager.gridSize && position.z >= 0 && position.z < gridManager.gridSize;
    }

    // Check if a node is an obstacle
    public bool IsObstacle(Node node)
    {
        int index = node.x * gridManager.gridSize + node.y;
        return obstacleData.obstacleGrid[index];
    }
}
