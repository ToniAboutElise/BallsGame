using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FillSurface : EditorWindow
{
    Object selectedSurface;
    Object assetToUpdate;
    string surfaceSpawnSceneObjectsButton = "Fill Surface With Scene GameObjects";
    string surfaceDeleteButton = "Clear Scene Objects";
    string clearSettersButton = "Clear Preparation Scripts";
    string updateAsset = "Update Asset";

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
        EditorGUILayout.Space(5);
        if (GUILayout.Button(surfaceSpawnSceneObjectsButton))
        {
            FillVerticesWithSceneObjects();
        }
        EditorGUILayout.Space(2);
        GUILayout.Label("Transform selected objects into any of the following options", EditorStyles.boldLabel);
        EditorGUILayout.Space(1);
        GUI.BeginGroup(new Rect(0, 0, 450, 350));

        var off = 20f;
        var px = 20f;
        var py = -15f;
        if(GUI.Button(new Rect(0 + off, 100 + py + off, 50, 50), "Wall"))
        {
            SpawnGameObjectInSelection("Level Creation/Environment/WallPrefab", 0.15f);
        }
        if (GUI.Button(new Rect(50 + px + off, 100 + py + off, 50, 50), "Orb"))
        {
            SpawnGameObjectInSelection("Level Creation/Collectables/OrbPrefab");
        }
        if (GUI.Button(new Rect(100 + px * 2f + off, 100 + py + off, 50, 50), "Bubble"))
        {
            SpawnGameObjectInSelection("Level Creation/Collectables/BubbledOrbPrefab");
        }
        if (GUI.Button(new Rect(150 + px * 3f + off, 100 + py + off, 50, 50), "Empty"))
        {
            SpawnGameObjectInSelection("Level Creation/Collectables/EmptySpacePrefab");
        }
        if (GUI.Button(new Rect(200 + px * 4f + off, 100 + py + off, 50, 50), "Door"))
        {
            SpawnGameObjectInSelection("Level Creation/Environment/DoorPrefab", 0.15f);
        }
        if (GUI.Button(new Rect(250 + px * 5f + off, 100 + py + off, 50, 50), "Fragile"))
        {
            SpawnGameObjectInSelection("Level Creation/Collectables/FragileOrbPrefab");
        }
        GUI.EndGroup();
        EditorGUILayout.Space(70);
        if (GUILayout.Button(clearSettersButton))
        {
            ClearPreparationScripts();
        }
        EditorGUILayout.Space(2);
        selectedSurface = EditorGUILayout.ObjectField(assetToUpdate, typeof(GameObject), true);
        if (GUILayout.Button(updateAsset))
        {
            UpdateAsset();
        }
        EditorGUILayout.Space(2);
        if (GUILayout.Button(surfaceDeleteButton))
        {
            ClearSurface();
        }


    }

    private void FillVerticesWithSceneObjects()
    {
        if (selectedSurface != null)
        {
            GameObject surfaceGO = (GameObject)selectedSurface;
            if(surfaceGO.transform.childCount != 0)
            {
                Debug.Log("This surface is not empty, clean it before instancing new objects onto it!");
                return;
            }
            Mesh mesh = surfaceGO.GetComponent<MeshFilter>().sharedMesh;
            Vector3[] vertices = mesh.vertices;
            for (var i = 0; i < vertices.Length; i++)
            {
                var direction = surfaceGO.transform.TransformPoint(vertices[i]);
                GameObject instance = Instantiate(Resources.Load<GameObject>("Level Creation/_Core/SelectionInstancingPrefab"), surfaceGO.transform);
                instance.transform.localPosition = vertices[i];
                if (i == vertices.Length - 1)
                {
                    Debug.Log(i + " objects instantiated");
                }
            }
        }
    }

    private void SpawnGameObjectInSelection(string gameObjectPath, float? height = 0.5f)
    {
        foreach (Transform selectedTransform in Selection.transforms)
        {
            if(selectedTransform.childCount != 0)
            {
                DestroyImmediate(selectedTransform.GetChild(0).gameObject);
            }

            GameObject instance = Instantiate(Resources.Load<GameObject>(gameObjectPath), selectedTransform.transform);
            instance.transform.localPosition = new Vector3(0, (float)height,0);
        }
    }

    private void ClearSurface()
    {
        GameObject surfaceGO = (GameObject)selectedSurface;

        for(int i = 0; i < surfaceGO.transform.childCount; i++)
        {
            DestroyImmediate(surfaceGO.transform.GetChild(i).gameObject);
        }
    }

    private void UpdateAsset()
    {
        GameObject newAsset = (GameObject)assetToUpdate;

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(newAsset.tag))
        {
            Transform gameObjectParent = gameObject.transform.parent;
            Vector3 gameObjectLocalPosition = gameObject.transform.localPosition;

            DestroyImmediate(gameObject);

            GameObject instance = Instantiate(newAsset, gameObjectParent);
            instance.transform.localPosition = gameObjectLocalPosition;
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