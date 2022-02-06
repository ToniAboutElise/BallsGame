using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Text _resolutionText;
    public Slider mouseSens;
    public Text aa;
    public GameObject Character;
    public GameObject myCanvas;
    public Toggle VsyncToggle;
    public Toggle AAPos;
    public Toggle WindowedToggle;
    bool isActive;
    bool Vsync;
    bool isWindowed;
    public GameObject FPSAudioListener;
    public GameObject flashlight;

    public void Windowed()
    {
        if (isWindowed)
        {
            Debug.Log("Windowed");
            Screen.fullScreen = true;
            isWindowed = false;
            WindowedToggle.isOn = false;
        }
        else
        {
            Debug.Log("NO Windowed");
            Screen.fullScreen = false;
            isWindowed = true;
            WindowedToggle.isOn = true;
        }
    }

    public void VsyncOn()
    {
        if (!Vsync)
        {
            Debug.Log("on");
            QualitySettings.vSyncCount = 1;
            Vsync = true;
        }
        else
        {
            Debug.Log("off");
            QualitySettings.vSyncCount = 0;
            Vsync = false;
        }
    }

    public void SetResolution()
    {
        string[] resolutionStrings = _resolutionText.text.Split('x');
        Screen.SetResolution(int.Parse(resolutionStrings[0]), int.Parse(resolutionStrings[1]), true);
    }

    public void SetAA()
    {
        if (aa.text == "No AA")
        {
            QualitySettings.antiAliasing = 0;
        }
        else if (aa.text == "2 SAMPLES")
        {
            QualitySettings.antiAliasing = 2;
        }
        else if (aa.text == "4 SAMPLES")
        {
            QualitySettings.antiAliasing = 4;
        }
        else if (aa.text == "8 SAMPLES")
        {
            QualitySettings.antiAliasing = 8;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("LevelSelectionMenu", LoadSceneMode.Single);
    }
}