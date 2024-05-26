using UnityEditor;
using UnityEngine;

public class DeletePrefs : EditorWindow
{
    [MenuItem("Tools/Player Prefs Remover")]
    public static void DeletePlayerPrefs()
    {
#if UNITY_EDITOR
        if (!EditorUtility.DisplayDialog("Delete Player Prefs", "Are you sure you want to delete all player preferences?", "Yes", "No"))
            return;
#endif
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted all player preferences."); // Changed log message for clarity
    }
}
