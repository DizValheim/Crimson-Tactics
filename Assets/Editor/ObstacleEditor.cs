using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObstacleData obstacleData = (ObstacleData)target;

        for (int row = 0; row < 10; row++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int col = 0; col < 10; col++)
            {
                obstacleData.blockedTiles[row,col] = EditorGUILayout.Toggle(obstacleData.blockedTiles[row,col]);
                
            }
            EditorGUILayout.EndHorizontal();
        }

        if(GUI.changed)
        {
            EditorUtility.SetDirty(obstacleData);
        }
    }
}
