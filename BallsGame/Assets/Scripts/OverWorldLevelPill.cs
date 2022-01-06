using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldLevelPill : MonoBehaviour
{
    public bool unlocked;
    public string scene;
    [SerializeField] private bool _isSelected = false;
    [SerializeField] private Animator _animator;

    public void SetSelected(bool selected)
    {
        _isSelected = selected;
        _animator.SetBool("selected", selected);
    }
}
