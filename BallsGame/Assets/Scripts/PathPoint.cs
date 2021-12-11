using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PointState pointState = PointState.Empty;

    public enum PointState
    {
        Empty,
        Filled
    }
}
