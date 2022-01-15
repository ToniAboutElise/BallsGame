using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class OverWorldManager : MonoBehaviour
{
    [SerializeField] private List<OverWorldWorld> overWorldWorlds = new List<OverWorldWorld>();
    public List<OverWorldLevelPill> _overWorldLevelPills = new List<OverWorldLevelPill>();
    [SerializeField] private OverWorldLevelPill currentSelectedPill;
    private string savedFile;
    private int currentSelectedPillInt;

    private void Start()
    {
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
                currentSelectedPillInt = currentLevelSet;
            }
            currentLevelSet++;
        }
        selectedPill.unlocked = true;
        selectedPill.SetSelected(true);
    }

    private void UpdateSelectedLevel()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentSelectedPill != _overWorldLevelPills[_overWorldLevelPills.Count - 1] && _overWorldLevelPills[currentSelectedPillInt + 1].unlocked == true)
            {
                currentSelectedPill.SetSelected(false);
                currentSelectedPillInt++;
                currentSelectedPill = _overWorldLevelPills[currentSelectedPillInt];
                currentSelectedPill.SetSelected(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentSelectedPill != _overWorldLevelPills[0] && _overWorldLevelPills[currentSelectedPillInt - 1].unlocked == true)
            {
                currentSelectedPill.SetSelected(false);
                currentSelectedPillInt--;
                currentSelectedPill = _overWorldLevelPills[currentSelectedPillInt];
                currentSelectedPill.SetSelected(true);
            }
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadSceneAsync(currentSelectedPill.levelNumber, LoadSceneMode.Single);
    }
    private void Update()
    {
        UpdateSelectedLevel();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel();
        }
    }
}