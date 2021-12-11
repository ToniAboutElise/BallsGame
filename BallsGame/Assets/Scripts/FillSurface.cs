using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FillSurface : EditorWindow
{
    Object selectedSurface;
    Object objectToSpawn;
    string surfaceSpawnButton = "Fill Surface";
    string surfaceDeleteButton = "Delete Surface Filling";

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
            FillSurfaceWithGameObject();
        }
        EditorGUILayout.Space(2);
        if (GUILayout.Button(surfaceDeleteButton))
        {
            DeleteSurfaceWithGameObject();
        }
    }

    private void FillSurfaceWithGameObject()
    {
        if(selectedSurface != null && FindObjectOfType<Ball>() == false)
        {
            GameObject surfaceGO = (GameObject)selectedSurface;
            Mesh mesh = surfaceGO.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;
            for (var i = 0; i < vertices.Length; i++)
            {
                var direction = surfaceGO.transform.TransformPoint(vertices[i]);
                GameObject instance = Instantiate((GameObject)objectToSpawn, surfaceGO.transform);
                instance.transform.localPosition = vertices[i];

                if(i == vertices.Length - 1)
                {
                    Debug.Log(i + " objects instantiated");
                }
            }
        }
    }

    private void DeleteSurfaceWithGameObject()
    {
        foreach(Ball ball in FindObjectsOfType<Ball>())
        {
            DestroyImmediate(ball.gameObject);
        }
    }
}
