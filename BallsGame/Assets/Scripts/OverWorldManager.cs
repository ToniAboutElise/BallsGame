using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldManager : MonoBehaviour
{
    [SerializeField] private List<OverWorldWorld> overWorldWorlds = new List<OverWorldWorld>();
    public List<OverWorldLevelPill> _overWorldlevelPills = new List<OverWorldLevelPill>();
    [SerializeField] private OverWorldLevelPill currentSelectedPill;
    private int currentSelectedPillInt;

    private void Start()
    {
        foreach(OverWorldWorld overWorldWorld in overWorldWorlds)
        {
            foreach (OverWorldLevelPill overWorldLevelPill in overWorldWorld.overWorldLevelPills)
            {
                _overWorldlevelPills.Add(overWorldLevelPill);
            }
        }

        SetCurrentSelectedLevel();
    }

    private void SetCurrentSelectedLevel()
    {
        for(int i = 0; i < _overWorldlevelPills.Count; i++)
        {
            if(i == _overWorldlevelPills.Count - 1 && _overWorldlevelPills[i].unlocked == true)
            {
                _overWorldlevelPills[i].SetSelected(true);
                currentSelectedPill = _overWorldlevelPills[i];
                currentSelectedPillInt = i;
            }

            if(_overWorldlevelPills[i].unlocked == false)
            {
                if(i != 0)
                {
                    _overWorldlevelPills[i - 1].SetSelected(true);
                    currentSelectedPill = _overWorldlevelPills[i - 1];
                    currentSelectedPillInt = i - 1;
                }
                else
                {
                    _overWorldlevelPills[i].SetSelected(true);
                    currentSelectedPill = _overWorldlevelPills[i];
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
            if(currentSelectedPill != _overWorldlevelPills[_overWorldlevelPills.Count - 1] && _overWorldlevelPills[currentSelectedPillInt + 1].unlocked == true)
            {
                currentSelectedPill.SetSelected(false);
                currentSelectedPillInt++;
                currentSelectedPill = _overWorldlevelPills[currentSelectedPillInt];
                currentSelectedPill.SetSelected(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentSelectedPill != _overWorldlevelPills[0] && _overWorldlevelPills[currentSelectedPillInt - 1].unlocked == true)
            {
                currentSelectedPill.SetSelected(false);
                currentSelectedPillInt--;
                currentSelectedPill = _overWorldlevelPills[currentSelectedPillInt];
                currentSelectedPill.SetSelected(true);
            }
        }
    }

    private void Update()
    {
        UpdateSelectedLevel();
    }
}
