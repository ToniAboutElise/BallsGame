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
    string getSelectedObjects = "Get Selected Objects";
    string clearSettersButton = "Clear Preparation Scripts";

    [MenuItem("Tools/Fill Surface")]
    private static void OpenVertexObjectGeneratorWindow()
    {
        FillSurface window = (FillSurface)EditorWindow.GetWindow(typeof(FillSurface));
        window.maxSize = new Vector2(500f, 500f);
        window.minSize = window.maxSize;
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
        GUILayout.Label("Transform selected objects into any of the following options", EditorStyles.boldLabel);
        EditorGUILayout.Space(1);
        GUI.BeginGroup(new Rect(0, 0, 350, 350));

        //GUI.Box(new Rect(250, 250, 80, 80), "Directions");

        var off = 20f;
        var px = 20f;
        var py = 20f;
        if(GUI.Button(new Rect(0 + off, 100 + py + off, 50, 50), "Wall"))
        {
            GetSelectedObjects();
        }
        if (GUI.Button(new Rect(50 + px + off, 100 + py + off, 50, 50), "Ball"))
        {
            GetSelectedObjects();
        }
        if (GUI.Button(new Rect(100 + px * 2f + off, 100 + py + off, 50, 50), "Bubble Ball"))
        {
            GetSelectedObjects();
        }
        GUI.EndGroup();
        EditorGUILayout.Space(70);
        if (GUILayout.Button(clearSettersButton))
        {
            ClearPreparationScripts();
        }
        EditorGUILayout.Space(2);
        if (GUILayout.Button(surfaceDeleteButton))
        {
            ClearSurface();
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

    private void GetSelectedObjects()
    {
        foreach (Transform transform in Selection.transforms)
        {
            Debug.Log(transform.name);
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
