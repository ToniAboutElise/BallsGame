using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbledCollectable : Collectable
{
    [SerializeField] private GameObject _bubble;
    public override void AdditionalEffect()
    {
        _bubble.SetActive(false);
        collectableState = CollectableState.NonCollected;
    }
}
