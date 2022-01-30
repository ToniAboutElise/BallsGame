using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CostumeButton : MonoBehaviour, ISelectHandler
{
    [HideInInspector] public CostumeShowcase costumeShowcase;
    public Costume costume;
    public void OnSelect(BaseEventData eventData)
    {
        LoadCostume();
        Debug.Log("TES");
    }

    public void SetCostume()
    {
        GameManagerSingleton.GetInstance().selectedCostume = costume;
    }

    private void LoadCostume()
    {
        costumeShowcase.LoadNewCostume(costume);
    }
}
