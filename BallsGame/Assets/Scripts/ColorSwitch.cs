using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Material _red;
    [SerializeField] private Material _blue;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private AudioClip _sfxColorSwitchAudioClip;

    private CurrentColor _currentColor = CurrentColor.Red;
    private enum CurrentColor
    {
        Blue,
        Red
    }

    public void SetNewColor()
    {
        switch (_currentColor)
        {
            case CurrentColor.Red:
                _currentColor = CurrentColor.Blue;
                _renderer.material = _red;
                break;
            case CurrentColor.Blue:
                _currentColor = CurrentColor.Red;
                _renderer.material = _blue;
                break;
        }
    }

    public void ColorSwitchAction(AudioSource sfxAudioSource = null)
    {
        sfxAudioSource.Stop();
        sfxAudioSource.clip = _sfxColorSwitchAudioClip;
        sfxAudioSource.Play();

        foreach (ColorSwitch colorSwitch in _levelManager.GetColorSwitches())
        {
            colorSwitch.SetNewColor();
        }

        foreach(ColorSwitchAffectedGameObject colorSwitchAffectedGameObject in _levelManager.GetColorSwitchAffectedGameObjects())
        {
            colorSwitchAffectedGameObject.ChangeState();
        }
    }
}
