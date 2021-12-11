using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObjectInstantiator))]
public class GameObjectInstantiatorEditor : Editor
{
    public GameObject ball;
    public GameObject obstacle;

    public ObjectToInstantiate objectToInstantiate;
    public enum ObjectToInstantiate
    {
        ball,
        obstacle
    }

    private GameObject GetGameObjectToInstantiate()
    {
        GameObject target = null;
        switch (objectToInstantiate)
        {
            case ObjectToInstantiate.ball:
                target = ball;
                break;
            case ObjectToInstantiate.obstacle:
                target = obstacle;
                break;
        }
        return target;
    }

    void OnSceneGUI()
    {
        Vector3 mousePosition = Event.current.mousePosition;
        //mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y;
        //mousePosition = sceneView.camera.ScreenToWorldPoint(mousePosition);
        //mousePosition.y = -mousePosition.y;

        Event e = Event.current;
        switch (e.type)
        {
            case EventType.KeyDown:
                {
                    if (Event.current.keyCode == (KeyCode.Space) && Event.current.button == 0)
                    {
                        GameObject.Instantiate(GetGameObjectToInstantiate(), mousePosition, GetGameObjectToInstantiate().transform.rotation);
                        // END EDIT
                    }
                    break;
                }
        }
    }
}
