using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

// Custom editor script to display file names in a dropdown menu
[CustomEditor(typeof(DataSyncer))]
public class VirtualGameObjectDropdown : Editor
{
    private string[] fileNames; // Array to store file names
    private int selectedFileIndex = 0; // Index of the selected file

    private void OnEnable()
    {
        RefreshFileNames();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Display dropdown menu
        EditorGUI.BeginChangeCheck();
        selectedFileIndex = EditorGUILayout.Popup(selectedFileIndex, fileNames);
        DataSyncer castTarget = (DataSyncer)target;
        if (EditorGUI.EndChangeCheck())
        {
            castTarget.typeName = fileNames[selectedFileIndex];
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(castTarget);
            EditorSceneManager.MarkSceneDirty(castTarget.gameObject.scene);
        }

    }

    private void RefreshFileNames()
    {
        // Debug.Log(SaveSystem.instance.virtualGameObjectDirectoryPath);
        string[] filePaths = Directory.GetFiles("Assets/SaveSystem/Example/VirtualGameObjects");

        fileNames = new string[filePaths.Length];
        for (int i = 0; i < filePaths.Length; i++)
        {
            if (filePaths[0].Contains(".meta"))
            {
                continue;
            }
            int index = filePaths[i].IndexOf(".");
            string filename = filePaths[i].Substring(0, index);
            fileNames[i] = Path.GetFileName(filename);
        }
    }
}