using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Text _resolutionText;
    [SerializeField] private TMP_Text _screenModeText; //?
    [SerializeField] private TMP_Dropdown _screenModeDropdown;
    [SerializeField] Toggle _vSyncToggle;
    [SerializeField] Toggle _aaToggle;

    public Slider mouseSens;
    public Text aa;
    public GameObject Character;
    public GameObject myCanvas;
    public Toggle WindowedToggle;
    bool isActive;
    bool Vsync;
    bool isWindowed;
    public GameObject FPSAudioListener;
    public GameObject flashlight;

    public void SetScreenMode()
    {
        switch (_screenModeDropdown.value)
        {
            case 0: //Full Screen
                Screen.fullScreen = true;
                isWindowed = false;
                break;
            case 1: //Full Screen Windowed
                Screen.fullScreen = true;
                isWindowed = true;
                break;
            case 2: //Windowed
                Screen.fullScreen = false;
                isWindowed = true;
                break;
        }
    }

    public void SetVSync()
    {
        switch (_vSyncToggle.isOn)
        { 
            case true:
                QualitySettings.vSyncCount = 1;
                break;
            case false:
                QualitySettings.vSyncCount = 0;
                break;
        }
    }

    public void SetResolution()
    {
        string[] resolutionStrings = _resolutionText.text.Split('x');
        Screen.SetResolution(int.Parse(resolutionStrings[0]), int.Parse(resolutionStrings[1]), true);
        SetScreenMode();
    }

    public void SetAA()
    {
        switch (_aaToggle.isOn)
        {
            case true:
                QualitySettings.antiAliasing = 4;
                break;
            case false:
                QualitySettings.antiAliasing = 0;
                break;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("LevelSelectionMenu", LoadSceneMode.Single);
    }
}