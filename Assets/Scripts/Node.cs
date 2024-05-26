// Node.cs
public class Node
{
    public int x;
    public int y;
    public bool isWalkable;
    public int gCost;
    public int hCost;
    public int fCost;
    public Node cameFromNode;
    public bool isOccupied;

    public Node(int x, int y, bool isWalkable)
    {
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
        this.isOccupied = false;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}