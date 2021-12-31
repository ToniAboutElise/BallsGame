using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int collectablesAmount;

    private void Start()
    {
        foreach(Collectable collectable in FindObjectsOfType<Collectable>())
        {
            if(collectable.GetCollectableState() != Collectable.CollectableState.Null)
            collectablesAmount++;
        }
    }

    public void CollectableGrabbed(Collectable collectable)
    {
        collectable.SetCollected();
        collectablesAmount--;
        CheckIfLevelIsCompleted();
    }

    private void CheckIfLevelIsCompleted()
    {
        if(collectablesAmount == 0)
        {
            //end level
        }
    }
}
