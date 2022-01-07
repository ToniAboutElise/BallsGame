using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class OverWorldLevelPill : MonoBehaviour
{
    public string levelNumber;
    public TMP_Text text;
    public bool unlocked;
    public string scene;
    public LevelPillMaterials levelPillMaterials;
    [System.Serializable] public struct LevelPillMaterials
    {
        public Material _unlocked;
        public Material _locked;
    }

    private LookAtConstraint _lookAtConstraint;
    [SerializeField] private bool _isSelected = false;
    [SerializeField] private Animator _animator;
    [SerializeField] private Renderer _renderer;

    

    public void SetSelected(bool selected)
    {
        _isSelected = selected;
        _animator.SetBool("selected", selected);
    }

    public void SetLockState(bool isUnlocked)
    {
        switch (unlocked)
        {
            case true:
                _renderer.material = levelPillMaterials._unlocked;
                break;
            case false:
                _renderer.material = levelPillMaterials._locked;
                break;
        }
    }
}
