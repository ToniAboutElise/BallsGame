using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OverWorldUI : MonoBehaviour
{
    [SerializeField] private PlayerInputActions _playerInputActions;
    [SerializeField] private Animator _overWorldAnimator;
    [SerializeField] private GameObject _levelSelectionUI;
    [SerializeField] private GameObject _customizeUI;
    [SerializeField] private Button _selectedFirstCustomizeButton;
    [SerializeField] private OverWorldManager _overWorldManager;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void CheckInputActions()
    {
        if (_playerInputActions.Player.L1.IsPressed())
        {
            _overWorldManager.currentOverWorldSection = OverWorldManager.CurrentOverWorldSection.LevelSelection;
            _overWorldAnimator.ResetTrigger("Customize");
            _overWorldAnimator.SetTrigger("LevelSelection");
            _customizeUI.SetActive(false);
            _levelSelectionUI.SetActive(true);
        }
        else if (_playerInputActions.Player.R1.IsPressed())
        {
            _overWorldManager.currentOverWorldSection = OverWorldManager.CurrentOverWorldSection.Customize;
            _overWorldAnimator.ResetTrigger("LevelSelection");
            _overWorldAnimator.SetTrigger("Customize");
            _levelSelectionUI.SetActive(false);
            _customizeUI.SetActive(true);
            _selectedFirstCustomizeButton.Select();
        }
    }

    private void FixedUpdate()
    {
        CheckInputActions();
    }
}
