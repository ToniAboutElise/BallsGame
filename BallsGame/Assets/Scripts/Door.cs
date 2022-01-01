using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredCollectables;
    [SerializeField] private Animation _animation;

    public void OpenDoor()
    {
        _animation.Play();
    }
}
