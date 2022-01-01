using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    [SerializeField] private int _requiredCollectables;
    [SerializeField] private Animation _animation;
    [SerializeField] private TMP_Text _requiredCollectablesText;

    private void Start()
    {
        _requiredCollectablesText.text = _requiredCollectables.ToString();
    }

    public void UpdateRequiredCollectables()
    {
        _requiredCollectables--;
        _requiredCollectablesText.text = _requiredCollectables.ToString();
        if (_requiredCollectables == 0)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        _animation.Play();
    }
}
