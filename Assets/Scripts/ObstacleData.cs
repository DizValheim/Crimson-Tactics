using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public bool[,] blockedTiles = new bool[10,10];
}
