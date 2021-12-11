using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectInstantiator : MonoBehaviour
{
    public GameObject ball;
    public GameObject obstacle;

    public ObjectToInstantiate objectToInstantiate;
    public enum ObjectToInstantiate
    {
        ball,
        obstacle
    }
}
