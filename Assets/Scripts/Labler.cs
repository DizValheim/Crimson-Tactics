using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Labler : MonoBehaviour
{
    public Vector2Int coords;
    GridManager gridManager;

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCoords();
        transform.name = $"({coords.x},{coords.y})";
    }

    void CalculateCoords()
    {
        if(!gridManager) { return; }
        coords.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        coords.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);
    }
}
