using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneObject : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Mesh _ballMesh;
    [SerializeField] private Mesh _obstacleMesh;

    [SerializeField] private SceneObjectType _sceneObjectType;
    private enum SceneObjectType
    {
        Ball,
        Obstacle
    }

    private void ChangeSceneObjectType()
    {
        switch (_sceneObjectType)
        {
            case SceneObjectType.Ball:
                _meshFilter.sharedMesh = _ballMesh;
                break;
            case SceneObjectType.Obstacle:
                _meshFilter.sharedMesh = _obstacleMesh;
                break;
        }
    }

    private void Update()
    {
        ChangeSceneObjectType();
    }
}
