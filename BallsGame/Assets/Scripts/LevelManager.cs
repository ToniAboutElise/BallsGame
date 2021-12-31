using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Text _ballsLeftText;
    private int collectablesAmount = 0;
    

    private void Awake()
    {
        foreach(Collectable collectable in FindObjectsOfType<Collectable>())
        {
            if(collectable.GetCollectableState() == Collectable.CollectableState.NonCollected)
            collectablesAmount++;
        }
        Debug.Log(collectablesAmount);
        _ballsLeftText.text = collectablesAmount.ToString();
    }

    public void CollectableGrabbed(Collectable collectable)
    {
        collectable.SetCollected();
        collectablesAmount--;
        _ballsLeftText.text = collectablesAmount.ToString();
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
