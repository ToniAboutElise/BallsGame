using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OverWorldManager : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] PlayerInputActions _playerInputActions;
    [SerializeField] private List<OverWorldWorld> overWorldWorlds = new List<OverWorldWorld>();
    public List<OverWorldLevelPill> _overWorldLevelPills = new List<OverWorldLevelPill>();
    [SerializeField] private OverWorldLevelPill currentSelectedPill;
    private string savedFile;
    private int _currentSelectedPillInt;
    private bool _canChangeSelectedLevel = true;
    private bool _loadingLevel = false;

    public CurrentOverWorldSection currentOverWorldSection = CurrentOverWorldSection.LevelSelection;
    public enum CurrentOverWorldSection
    {
        LevelSelection,
        Customize
    }

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        ResetLevelsUnlocked();
        CheckUnlockedLevels();
    }

    private void ResetLevelsUnlocked()
    {
        int currentOverWorld = 0;
        int currentLevel = 0;
        foreach (OverWorldWorld overWorldWorld in overWorldWorlds)
        {
            currentOverWorld++;
            foreach (OverWorldLevelPill overWorldLevelPill in overWorldWorld.overWorldLevelPills)
            {
                currentLevel++;
                overWorldLevelPill.levelNumber = currentOverWorld + "-" + currentLevel;
                overWorldLevelPill.text.text = overWorldLevelPill.levelNumber;
                overWorldLevelPill.SetLockState(overWorldLevelPill.unlocked);
                _overWorldLevelPills.Add(overWorldLevelPill);
            }
        }
    }

    private void CheckUnlockedLevels()
    {
        savedFile = Application.persistentDataPath + "\\savedFile.txt";
        int currentOverWorld = 0;
        int currentLevel = 0;
        if (!File.Exists(savedFile))
        {
            using (FileStream fs = File.Create(savedFile))
            {
                foreach (OverWorldWorld overWorldWorld in overWorldWorlds)
                {
                    currentOverWorld++;
                    foreach (OverWorldLevelPill overWorldLevelPill in overWorldWorld.overWorldLevelPills)
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(overWorldLevelPill.levelNumber + " " + overWorldLevelPill.unlocked + "\n");
                        currentLevel++;
                        fs.Write(info, 0, info.Length);
                    }
                }
                
                fs.Close();
            }
        }
        Debug.Log(savedFile);

        List<string> levelsToSet = new List<string>();
        foreach (string line in File.ReadAllLines(savedFile))
        {
            levelsToSet.Add(line);
        }

        int currentLevelSet = 0;
        OverWorldLevelPill selectedPill = null;
        foreach(string level in levelsToSet)
        {
            string unlocked = level.Substring(level.Length - 5);
            if(unlocked == "False")
            {
                _overWorldLevelPills[currentLevelSet].unlocked = false;
                _overWorldLevelPills[currentLevelSet].SetLockState(false);
            }
            else
            {
                _overWorldLevelPills[currentLevelSet].unlocked = true;
                _overWorldLevelPills[currentLevelSet].SetLockState(true);
                selectedPill = _overWorldLevelPills[currentLevelSet];
                currentSelectedPill = _overWorldLevelPills[currentLevelSet];
                _currentSelectedPillInt = currentLevelSet;
            }
            currentLevelSet++;
        }

        if (_overWorldLevelPills[0].unlocked == false)
        {
            selectedPill = _overWorldLevelPills[0];
            currentSelectedPill = _overWorldLevelPills[0];
            _currentSelectedPillInt = 0;
        }

        selectedPill.unlocked = true;
        selectedPill.SetSelected(true);
        selectedPill.SetLockState(true);


    }

    private void UpdateSelectedLevel()
    {

        if (_playerInputActions.Player.Move.ReadValue<Vector2>().x > 0.3f && _canChangeSelectedLevel == true)
        {
            _canChangeSelectedLevel = false;
            if (currentSelectedPill != _overWorldLevelPills[_overWorldLevelPills.Count - 1] && _overWorldLevelPills[_currentSelectedPillInt + 1].unlocked == true)
            {
                currentSelectedPill.SetSelected(false);
                _currentSelectedPillInt++;
                currentSelectedPill = _overWorldLevelPills[_currentSelectedPillInt];
                currentSelectedPill.SetSelected(true);
            }
        }
        else if (_playerInputActions.Player.Move.ReadValue<Vector2>().x < -0.3f && _canChangeSelectedLevel == true)
        {
            _canChangeSelectedLevel = false;
            if (currentSelectedPill != _overWorldLevelPills[0] && _overWorldLevelPills[_currentSelectedPillInt - 1].unlocked == true)
            {
                currentSelectedPill.SetSelected(false);
                _currentSelectedPillInt--;
                currentSelectedPill = _overWorldLevelPills[_currentSelectedPillInt];
                currentSelectedPill.SetSelected(true);
            }
        }
        else if (_playerInputActions.Player.Move.ReadValue<Vector2>().x > -0.15f && _playerInputActions.Player.Move.ReadValue<Vector2>().x < 0.15f)
        {
            _canChangeSelectedLevel = true;
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadSceneAsync(currentSelectedPill.levelNumber, LoadSceneMode.Single);
    }
    private void Update()
    {
        UpdateSelectedLevel();
        
        if ( _playerInputActions.Player.FaceButtonDown.IsPressed() && currentOverWorldSection == CurrentOverWorldSection.LevelSelection && _loadingLevel == false)
        {
            _loadingLevel = true;
            LoadLevel();
        }
        
    }
}