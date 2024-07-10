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
    }


    private void Update() {
        InstantiateObstacles();
    }
    private void InstantiateObstacles()
    {
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {

                if (obstacleData.blockedTiles[row, col])
                {
                    // Debug.Log($"Obstacle instantiated at ({row}, {col})");
                    Vector3 worldPos = CalculateWorldPosition(row, col);
                    Instantiate(obstaclePrefab, worldPos, Quaternion.identity);
                }

            }
        }
    }

    private Vector3 CalculateWorldPosition(int row, int col)
    {
        float tileSize = gridManager.UnityGridSize;
        // Vector3 offset = new Vector3(tileSize / 2f, 0f, tileSize / 2f); // Center of each tile

        Vector3 worldPos = new Vector3(row * tileSize, 0.5f, col * tileSize);
        return worldPos;
    }
}