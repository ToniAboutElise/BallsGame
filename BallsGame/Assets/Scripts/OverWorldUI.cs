using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OverWorldUI : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerInputActions _playerInputActions;
    [SerializeField] private Animator _overWorldAnimator;
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _levelSelectionUI;
    [SerializeField] private GameObject _customizeUI;
    [SerializeField] private Text _customizeKeyText;
    [SerializeField] private Text _levelsKeyText;
    [SerializeField] private Button _selectedFirstCustomizeButton;
    [SerializeField] private OverWorldManager _overWorldManager;
    [SerializeField] private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInput.currentActionMap = _playerInputActions.Player;
        _customizeUI.SetActive(false);
    }

    private void CheckInputActions()
    {
        if(_overWorldManager.currentOverWorldSection != OverWorldManager.CurrentOverWorldSection.MainMenu) 
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
    }

    public void GoToSection()
    {
        switch (_overWorldManager.currentOverWorldSection)
        {
            case OverWorldManager.CurrentOverWorldSection.LevelSelection:
                _overWorldManager.currentOverWorldSection = OverWorldManager.CurrentOverWorldSection.MainMenu;
                _overWorldAnimator.ResetTrigger("LevelSelection");
                _overWorldAnimator.SetTrigger("MainMenu");
                _mainMenuUI.SetActive(true);
                _levelSelectionUI.SetActive(false);
                break;
            case OverWorldManager.CurrentOverWorldSection.MainMenu:
                _overWorldManager.currentOverWorldSection = OverWorldManager.CurrentOverWorldSection.LevelSelection;
                _overWorldAnimator.ResetTrigger("MainMenu");
                _overWorldAnimator.SetTrigger("LevelSelection");
                _mainMenuUI.SetActive(false);
                _levelSelectionUI.SetActive(true);
                break;
        }
    }

    private void RotatePlayerShowcase()
    {
        _player.transform.Rotate(0, -_playerInputActions.Player.Camera.ReadValue<Vector2>().x*5, 0, Space.Self);
    }

    private void AdaptUIKeysToLastInput()
    {
        switch (_playerInput.currentControlScheme)
        {
            case "Keyboard&Mouse":
                _levelsKeyText.text = "Q";
                _customizeKeyText.text = "E";
                break;
            case "Gamepad":
                _levelsKeyText.text = "L";
                _customizeKeyText.text = "R";
                break;
        }

    }

    private void FixedUpdate()
    {
        CheckInputActions();
        RotatePlayerShowcase();
        AdaptUIKeysToLastInput();
    }
}
