using UnityEditor;
using UnityEngine;

// Custom editor for the ObstacleData scriptable object
[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    // Override the default Inspector GUI
    public override void OnInspectorGUI()
    {
        // Cast the target object to ObstacleData type
        ObstacleData data = (ObstacleData)target;

        // Loop through rows
        for (int i = 0; i < 10; i++)
        {
            GUILayout.BeginHorizontal(); // Begin horizontal layout group for each row
            // Loop through columns
            for (int j = 0; j < 10; j++)
            {
                // Calculate the index in the obstacleGrid array
                int index = i * 10 + j;
                // Create a toggle for each grid cell
                data.obstacleGrid[index] = GUILayout.Toggle(data.obstacleGrid[index], GUIContent.none, GUILayout.Width(20), GUILayout.Height(20));
            }
            GUILayout.EndHorizontal(); // End horizontal layout group for the row
        }
        
        // Check if any GUI elements changed
        if (GUI.changed)
        {
            // Mark the object as dirty so that changes are saved
            EditorUtility.SetDirty(data);
        }
    }
}
