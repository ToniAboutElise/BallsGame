using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FillSurface : EditorWindow
{
    Object pathPointPrefab;
    Object selectedSurface;
    Object ball;
    Object obstacle;
    string surfacePrepareButton = "Prepare Surface";
    string surfaceSpawnBallsButton = "Fill Surface With Balls";
    string surfaceSpawnObstaclesButton = "Fill Surface With Obstacles";
    string surfaceDeleteButton = "Delete Surface Filling";

    private List<PathPoint> pathPointsList = new List<PathPoint>();
    private List<Ball> ballsList = new List<Ball>();
    private List<Obstacle> obstaclesList = new List<Obstacle>();

    [MenuItem("Tools/Fill Surface")]
    private static void OpenVertexObjectGeneratorWindow()
    {
        FillSurface window = (FillSurface)EditorWindow.GetWindow(typeof(FillSurface));
        window.Show();
    }

    void OnGUI()
    {
        CheckObjectsAlreadyInScene();
        EditorGUILayout.Space();
        GUILayout.Label("Selected Surface", EditorStyles.boldLabel);
        selectedSurface = EditorGUILayout.ObjectField(selectedSurface, typeof(GameObject), true);
        EditorGUILayout.Space(2);
        GUILayout.Label("Path Point prefab", EditorStyles.boldLabel);
        pathPointPrefab = EditorGUILayout.ObjectField(pathPointPrefab, typeof(GameObject), true);
        EditorGUILayout.Space(2);
        GUILayout.Label("Ball prefab", EditorStyles.boldLabel);
        ball = EditorGUILayout.ObjectField(ball, typeof(GameObject), true);
        EditorGUILayout.Space(2);
        GUILayout.Label("Obstacle prefab", EditorStyles.boldLabel);
        obstacle = EditorGUILayout.ObjectField(obstacle, typeof(GameObject), true);
        EditorGUILayout.Space(3);
        if (GUILayout.Button(surfacePrepareButton))
        {
            FillPathPoints();
        }
        EditorGUILayout.Space(2);
        if (GUILayout.Button(surfaceSpawnBallsButton))
        {
            FillSurfaceWithObject(ball);
        }
        EditorGUILayout.Space(2);
        if (GUILayout.Button(surfaceSpawnObstaclesButton))
        {
            FillSurfaceWithObject(obstacle);
        }
        EditorGUILayout.Space(2);
        if (GUILayout.Button(surfaceDeleteButton))
        {
            ClearSurface();
        }

        
    }

    private void FillPathPoints()
    {
        Debug.Log(pathPointsList.Count);
        if (selectedSurface != null && pathPointsList != null)
        {
            GameObject surfaceGO = (GameObject)selectedSurface;
            Mesh mesh = surfaceGO.GetComponent<MeshFilter>().sharedMesh;
            Vector3[] vertices = mesh.vertices;
            for (var i = 0; i < vertices.Length; i++)
            {
                var direction = surfaceGO.transform.TransformPoint(vertices[i]);
                GameObject instance = Instantiate((GameObject)pathPointPrefab, surfaceGO.transform);
                instance.transform.localPosition = vertices[i];
                pathPointsList.Add(instance.GetComponent<PathPoint>());
                if (i == vertices.Length - 1)
                {
                    Debug.Log(i + " objects instantiated");
                }
            }
        }
    }

    private void CheckObjectsAlreadyInScene()
    {
        if(pathPointsList.Count == 0)
        {
            foreach(PathPoint pathPoint in FindObjectsOfType<PathPoint>())
            {
                pathPointsList.Add(pathPoint);
            }
        }

        if(pathPointsList.Count != 0)
        {
            foreach (Ball ball in FindObjectsOfType<Ball>())
            {
                ballsList.Add(ball);
            }

            foreach (Obstacle obstacle in FindObjectsOfType<Obstacle>())
            {
                obstaclesList.Add(obstacle);
            }
        }
    }

    private void FillSurfaceWithObject(Object objectToSpawn)
    {
        for(int i = 0; i < pathPointsList.Count; i++)
        {
            if(pathPointsList[i].pointState == PathPoint.PointState.Empty) 
            { 
                GameObject instance = Instantiate((GameObject)objectToSpawn, pathPointsList[i].transform);
                instance.transform.localPosition = Vector3.zero;
                pathPointsList[i].pointState = PathPoint.PointState.Filled;
            }
        }
    }

    private void ClearSurface()
    {
        foreach (PathPoint pathPoint in FindObjectsOfType<PathPoint>())
        {
            DestroyImmediate(pathPoint.gameObject);
            
        }

        foreach (Ball ball in FindObjectsOfType<Ball>())
        {
            DestroyImmediate(ball.gameObject);
            
        }

        foreach (Obstacle obstacle in FindObjectsOfType<Obstacle>())
        {
            DestroyImmediate(obstacle.gameObject);
            
        }

        pathPointsList.Clear();
        ballsList.Clear();
        obstaclesList.Clear();
    }
}
