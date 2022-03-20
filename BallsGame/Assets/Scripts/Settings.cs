using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Text _resolutionText;
    [SerializeField] private TMP_Text _screenModeText;
    [SerializeField] private TMP_Dropdown _screenModeDropdown;
    [SerializeField] private Toggle _vSyncToggle;
    [SerializeField] private Toggle _aaToggle;

    public void SetScreenMode()
    {
        switch (_screenModeDropdown.value)
        {
            case 0:
                Screen.fullScreen = true;
                break;
            case 1:
                Screen.fullScreen = true;
                break;
            case 2:
                Screen.fullScreen = false;
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

    public void QuitApp()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("LevelSelectionMenu", LoadSceneMode.Single);
    }
}