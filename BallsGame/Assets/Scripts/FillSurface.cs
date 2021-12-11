using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FillSurface : EditorWindow
{
    Object selectedSurface;
    Object objectToSpawn;
    string surfaceSpawnButton = "Fill Surface";

    [MenuItem("Tools/Fill Surface")]
    private static void OpenVertexObjectGeneratorWindow()
    {
        FillSurface window = (FillSurface)EditorWindow.GetWindow(typeof(FillSurface));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        GUILayout.Label("Selected Surface", EditorStyles.boldLabel);
        selectedSurface = EditorGUILayout.ObjectField(selectedSurface, typeof(GameObject), true);
        EditorGUILayout.Space(2);
        GUILayout.Label("Object to Spawn along vertices from object above", EditorStyles.boldLabel);
        objectToSpawn = EditorGUILayout.ObjectField(objectToSpawn, typeof(GameObject), true);
        EditorGUILayout.Space(3);
        if (GUILayout.Button(surfaceSpawnButton))
        {

        }
    }
}
