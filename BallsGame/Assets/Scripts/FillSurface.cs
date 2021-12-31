using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FillSurface : EditorWindow
{
    Object selectedSurface;
    Object sceneObject;
    string surfaceSpawnSceneObjectsButton = "Fill Surface With Scene GameObjects";
    string surfaceDeleteButton = "Clear Scene Objects";
    string clearSettersButton = "Clear Preparation Scripts";

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
        GUILayout.Label("Scene Object prefab", EditorStyles.boldLabel);
        sceneObject = EditorGUILayout.ObjectField(sceneObject, typeof(GameObject), true);
        EditorGUILayout.Space(3);
        if (GUILayout.Button(surfaceSpawnSceneObjectsButton))
        {
            FillVerticesWithSceneObjects();
        }
        EditorGUILayout.Space(2);
        EditorGUILayout.Space(2);
        EditorGUILayout.Space(2);
        if (GUILayout.Button(surfaceDeleteButton))
        {
            ClearSurface();
        }
        EditorGUILayout.Space(2);
        if (GUILayout.Button(clearSettersButton))
        {
            ClearPreparationScripts();
        }


    }

    private void FillVerticesWithSceneObjects()
    {
        if (selectedSurface != null && FindObjectOfType<SceneObject>() == null)
        {
            GameObject surfaceGO = (GameObject)selectedSurface;
            Mesh mesh = surfaceGO.GetComponent<MeshFilter>().sharedMesh;
            Vector3[] vertices = mesh.vertices;
            for (var i = 0; i < vertices.Length; i++)
            {
                var direction = surfaceGO.transform.TransformPoint(vertices[i]);
                GameObject instance = Instantiate((GameObject)sceneObject, surfaceGO.transform);
                instance.transform.localPosition = vertices[i];
                if (i == vertices.Length - 1)
                {
                    Debug.Log(i + " objects instantiated");
                }
            }
        }
    }

    private void ClearSurface()
    {
        foreach (SceneObject sceneObject in FindObjectsOfType<SceneObject>())
        {
            DestroyImmediate(sceneObject.transform.parent.gameObject);
            
        }
    }

    private void ClearPreparationScripts()
    {
        foreach(SceneObject sceneObject in FindObjectsOfType<SceneObject>())
        {
            DestroyImmediate(sceneObject);
        }
    }
}
