using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObstacleData obstacleData = (ObstacleData)target;
        if (obstacleData == null)
            return;

        if (obstacleData.blockedTiles == null)
            return;

        for (int row = 0; row < 10; row++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int col = 0; col < 10; col++)
            {
                obstacleData.blockedTiles.columns[row].row[col] = EditorGUILayout.Toggle(obstacleData.blockedTiles.columns[row].row[col]);
                
            }
            EditorGUILayout.EndHorizontal();
        }

        if(GUI.changed)
        {
            EditorUtility.SetDirty(obstacleData);
        }
    }
}