using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileCollectable : Collectable
{
    [SerializeField] private MeshRenderer _meshRenderer;
    public override void AdditionalEffect()
    {
        _meshRenderer.enabled = false;
        FindObjectOfType<LevelManager>().CollectableGrabbed(this);
        collectableState = CollectableState.Null;
    }
}
