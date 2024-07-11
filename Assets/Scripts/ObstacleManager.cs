using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [Tooltip("Prefab of the obstacle.")]
    [SerializeField] GameObject obstaclePrefab;

    [Tooltip("Obstacle configuration")]
    public ObstacleData obstacleData;

    GridManager gridManager;

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        InstantiateObstacles();
    }

    /// <summary>Spawns Obstacles according to ObstacleData </summary>
    private void InstantiateObstacles()
    {
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {

                if (obstacleData.blockedTiles.columns[col].row[row])
                {
                    Vector3 worldPos = gridManager.GetPositionFromCoordinates(new Vector2Int(row,col));
                    worldPos.y = 0.5f;  // to spawn above the tile
                    Instantiate(obstaclePrefab, worldPos, Quaternion.identity);
                    gridManager.BlockNode(new Vector2Int(row, col));
                }

            }
        }
    }

}
