using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    public Vector2Int StartCoords { get {return startCoords; } }

    [SerializeField] Vector2Int targetCoords;
    public Vector2Int TargetCoords { get {return targetCoords; } }

    Node startNode;
    Node targetNode;
    Node currentNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    Vector2Int[] searchOrder = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoords);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();

        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.walkable = true;
        targetNode.walkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentNode = frontier.Dequeue();
            currentNode.explored = true;
            ExploreNeighbors();
            if(currentNode.coords == targetCoords)
            {
                isRunning = false;
            }
        }

    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in searchOrder)
        {
            Vector2Int neighborCoords = currentNode.coords + direction;

            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coords) && neighbor.walkable)
            {
                neighbor.connectedTo = currentNode;
                reached.Add(neighbor.coords, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        path.Add(currentNode);
        currentNode.path = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.path = true;
        }

        path.Reverse();
        return path;

    }

    public void NotifyReceievers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public void SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates)
    {
        startCoords = startCoordinates;
        targetCoords = targetCoordinates;
        startNode = grid[this.startCoords];
        targetNode = grid[this.targetCoords];
        GetNewPath();
    }

}
