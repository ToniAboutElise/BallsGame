using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Material _red;
    [SerializeField] private Material _blue;
    [SerializeField] private Renderer _renderer;

    private CurrentColor _currentColor = CurrentColor.Red;
    private enum CurrentColor
    {
        Blue,
        Red
    }

    private void Start()
    {
        
    }

    public void SetNewColor()
    {
        switch (_currentColor)
        {
            case CurrentColor.Red:
                _currentColor = CurrentColor.Blue;
                _renderer.material = _blue;
                break;
            case CurrentColor.Blue:
                _currentColor = CurrentColor.Red;
                _renderer.material = _red;
                break;
        }
    }

    public void ColorSwitchAction()
    {
        foreach(ColorSwitch colorSwitch in _levelManager.GetColorSwitches())
        {
            colorSwitch.SetNewColor();
        }

        foreach(ColorSwitchAffectedGameObject colorSwitchAffectedGameObject in _levelManager.GetColorSwitchAffectedGameObjects())
        {
            colorSwitchAffectedGameObject.ChangeState();
        }
    }
}
