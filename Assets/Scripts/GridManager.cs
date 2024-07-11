using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Tooltip("Size of the grid.")]
    [SerializeField] Vector2Int gridSize;

    [Tooltip("Unity grid snap value")]
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

    /// <summary>Sets the walkable boolean to false of the tile at the given coordinate.</summary>
    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
            grid[coordinates].walkable = false;
    }

    /// <summary>Resets the node values.</summary>
    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.explored = false;
            entry.Value.path = false;
        }
    }

    /// <summary>Converts Vector3 world position to Vector2Int coordinates</summary>
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int
        {
            x = Mathf.RoundToInt(position.x / unityGridSize),
            y = Mathf.RoundToInt(position.z / unityGridSize)
        };

        return coordinates;
    }

    /// <summary>Converts Vector2Int coordinates to Vector3 world position</summary>
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3
        {
            x = coordinates.x * unityGridSize,
            z = coordinates.y * unityGridSize
        };
        
        return position;
    }

    /// <summary>Creates Grid based on grid size.</summary>
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
