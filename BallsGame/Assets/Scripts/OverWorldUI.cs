using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldUI : MonoBehaviour
{
    [SerializeField] PlayerInputActions _playerInputActions;
    [SerializeField] Animator _overWorldAnimator;

    [SerializeField] private GameObject _levelSelectionUI;
    [SerializeField] private GameObject _customizeUI;

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
    }

    private void CheckInputActions()
    {
        if (_playerInputActions.Player.L1.IsPressed())
        {
            _overWorldAnimator.ResetTrigger("Customize");
            _overWorldAnimator.SetTrigger("LevelSelection");
            _customizeUI.SetActive(false);
            _levelSelectionUI.SetActive(true);
        }
        else if (_playerInputActions.Player.R1.IsPressed())
        {
            _overWorldAnimator.ResetTrigger("LevelSelection");
            _overWorldAnimator.SetTrigger("Customize");
            _levelSelectionUI.SetActive(false);
            _customizeUI.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        CheckInputActions();
    }
}
