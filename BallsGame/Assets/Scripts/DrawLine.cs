using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DrawLine : MonoBehaviour
{
    // an array of game objects which will have a
    // line drawn to in the Scene editor
    public GameObject[] GameObjects;
    public GameObject a;

    //Instantiate a spawnPoint at the middle of the screen on the object that raycast hit
    void SpawnInstantiating()
    {
        Selection.activeObject = SceneView.currentDrawingSceneView;
        Camera sceneCam = SceneView.currentDrawingSceneView.camera;
        Vector3 rayPos = sceneCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1000f));
        Ray ray = new Ray(sceneCam.transform.position, rayPos);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("E");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("AAA");
            }
        }
    }


    private void Update()
    {/*
        if (Input.GetMouseButtonDown(0)) 
        { 
            GameObject clone = (GameObject)Instantiate(a, Input.mousePosition, a.transform.rotation);
        }
        */
        //SpawnInstantiating();
    }
}