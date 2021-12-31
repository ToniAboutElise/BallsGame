using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _nonCollected;
    [SerializeField] private Material _collected;

    [SerializeField]private CollectableState collectableState = CollectableState.NonCollected;
    public enum CollectableState
    {
        NonCollected,
        Collected,
        Null
    }

    public void SetCollected()
    {
        if(collectableState == CollectableState.NonCollected)
        {
            _renderer.material = _collected;
            collectableState = CollectableState.Collected;
        }
    }

    public CollectableState GetCollectableState()
    {
        return collectableState;
    }
}
