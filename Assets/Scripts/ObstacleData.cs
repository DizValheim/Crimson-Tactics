using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
     public TableData blockedTiles = new TableData(10);
}

[System.Serializable]
public class TableData
{
    public Row[] columns;

    public TableData(int size)
    {
        columns = new Row[size];
        for (int i = 0; i < size; i++)
            columns[i] = new Row(size);
    }
    [System.Serializable]
    public class Row
    {
        public bool[] row;
        public Row(int rows)
        {
            row = new bool[rows];
        }    
    }
}