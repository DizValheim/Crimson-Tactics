using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Holds data about each individual node in the grid.</summary>
public class Node
{
    public Vector2Int coords;
    public bool walkable;
    public bool explored;
    public bool path;
    public Node connectedTo;

    public Node (Vector2Int coords, bool walkable)
    {
        this.coords = coords;
        this.walkable = walkable;
    }
}
