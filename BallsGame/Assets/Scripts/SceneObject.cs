using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneObject : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Mesh _ballMesh;
    [SerializeField] private Mesh _obstacleMesh;

    [SerializeField] private Material _obstacleMaterial;
    [SerializeField] private Material _ballMaterial;

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
                _meshFilter.GetComponent<MeshRenderer>().sharedMaterial = _ballMaterial;
                _meshFilter.GetComponent<BoxCollider>().isTrigger = false;
                break;
            case SceneObjectType.Obstacle:
                _meshFilter.sharedMesh = _obstacleMesh;
                _meshFilter.GetComponent<MeshRenderer>().sharedMaterial = _obstacleMaterial;
                _meshFilter.GetComponent<BoxCollider>().isTrigger = true;
                break;
        }
    }

    private void Update()
    {
        ChangeSceneObjectType();
    }
}
