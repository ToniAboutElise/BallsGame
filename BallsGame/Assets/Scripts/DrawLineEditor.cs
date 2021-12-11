using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawLine))]
public class DrawLineEditor : Editor
{
    // draw lines between a chosen game object
    // and a selection of added game objects

    void OnSceneGUI()
    {
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.KeyDown:
                {
                    if (Event.current.keyCode == (KeyCode.Space) && Event.current.button == 0)
                    {
                        Debug.Log("Hi");
                        //GameObject instance = Instantiate((GameObject)pathPointPrefab);

                        e.Use();
                        // END EDIT
                    }
                    break;
                }
        }
    }
}