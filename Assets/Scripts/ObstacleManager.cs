using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField] GameObject obstaclePrefab;
    public ObstacleData obstacleData;

    GridManager gridManager;

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        InstantiateObstacles();
    }
    private void InstantiateObstacles()
    {
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {

                if (obstacleData.blockedTiles.columns[col].row[row])
                {
                    Vector3 worldPos = CalculateWorldPosition(row, col);
                    Instantiate(obstaclePrefab, worldPos, Quaternion.identity);
                }

            }
        }
    }

    private Vector3 CalculateWorldPosition(int row, int col)
    {
        float tileSize = gridManager.UnityGridSize;

        Vector3 worldPos = new Vector3(row * tileSize, 0.5f, col * tileSize);
        return worldPos;
    }
}
