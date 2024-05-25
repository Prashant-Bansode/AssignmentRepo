using UnityEngine;

public class Node
{
    // Coordinates of the node in the grid
    public int x;
    public int y;
    
    // Indicates whether the node is walkable or not
    public bool isWalkable;
    
    // Costs used in pathfinding algorithms
    public int gCost; // Cost from start node to this node
    public int hCost; // Heuristic cost from this node to target node
    public int fCost; // Total estimated cost (gCost + hCost)
    
    // Reference to the previous node in the path
    public Node cameFromNode;
    
    // Position of the node in the world space
    public Vector3 position;

    // Constructor to initialize the node
    public Node(int x, int y, bool isWalkable)
    {
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
        // Calculate the position based on grid coordinates
        this.position = new Vector3(x, 0, y);
    }

    // Calculate the total cost (fCost) of the node
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
