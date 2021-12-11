using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MyEditorPlayer))]
public class MyEditorPlayer : Editor
{
    void OnSceneGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hi");
        }
    }
}