using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [SerializeField] int unityGridSize;
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get {return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
            return grid[coordinates];
        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
            grid[coordinates].walkable = false;
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.explored = false;
            entry.Value.path = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int
        {
            x = Mathf.RoundToInt(position.x / unityGridSize),
            y = Mathf.RoundToInt(position.z / unityGridSize)
        };

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3
        {
            x = coordinates.x * unityGridSize,
            z = coordinates.y * unityGridSize
        };
        
        return position;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coords = new Vector2Int(x, y);
                grid.Add(coords, new Node(coords, true));
            }
        }
    }
}
