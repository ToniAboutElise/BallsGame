using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class OverWorldManager : MonoBehaviour
{
    [SerializeField] private List<OverWorldWorld> overWorldWorlds = new List<OverWorldWorld>();
    public List<OverWorldLevelPill> _overWorldLevelPills = new List<OverWorldLevelPill>();
    [SerializeField] private OverWorldLevelPill currentSelectedPill;
    private int currentSelectedPillInt;

    private void Start()
    {
        int currentOverWorld = 0;
        int currentLevel = 0;
        foreach(OverWorldWorld overWorldWorld in overWorldWorlds)
        {
            currentOverWorld++;
            foreach (OverWorldLevelPill overWorldLevelPill in overWorldWorld.overWorldLevelPills)
            {
                currentLevel++;
                overWorldLevelPill.levelNumber = currentOverWorld + "-" + currentLevel;
                overWorldLevelPill.text.text = overWorldLevelPill.levelNumber;
                overWorldLevelPill.SetLockState(overWorldLevelPill.unlocked);
                _overWorldLevelPills.Add(overWorldLevelPill);
                //Set unlocked pills by reading saved file here
            }
        }

        SetCurrentSelectedLevel();
    }

    private void SetCurrentSelectedLevel()
    {
        for(int i = 0; i < _overWorldLevelPills.Count; i++)
        {
            if (i == _overWorldLevelPills.Count - 1 && _overWorldLevelPills[i].unlocked == true)
            {
                _overWorldLevelPills[i].SetSelected(true);
                currentSelectedPill = _overWorldLevelPills[i];
                currentSelectedPillInt = i;
            }

            if(_overWorldLevelPills[i].unlocked == false)
            {
                if(i != 0)
                {
                    _overWorldLevelPills[i - 1].SetSelected(true);
                    currentSelectedPill = _overWorldLevelPills[i - 1];
                    currentSelectedPillInt = i - 1;
                }
                else
                {
                    _overWorldLevelPills[i].SetSelected(true);
                    currentSelectedPill = _overWorldLevelPills[i];
                    currentSelectedPillInt = i;
                }
                return;
            }
        }
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
