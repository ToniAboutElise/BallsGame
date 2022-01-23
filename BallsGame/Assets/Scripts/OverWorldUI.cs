using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldUI : MonoBehaviour
{
    [SerializeField] PlayerInputActions _playerInputActions;
    [SerializeField] Animator _overWorldAnimator;

    public CurrentOverWorldSection currentOverWorldSection = CurrentOverWorldSection.LevelSelection;
    public enum CurrentOverWorldSection
    {
        LevelSelection,
        Customize
    }

    private void CheckInputActions()
    {
        if (_playerInputActions.Player.L1.IsPressed())
        {
            _overWorldAnimator.SetTrigger("LevelSelection");
        }
        else if (_playerInputActions.Player.R1.IsPressed())
        {
            _overWorldAnimator.SetTrigger("Customize");
        }
    }

    private void FixedUpdate()
    {
        CheckInputActions();
    }
}
