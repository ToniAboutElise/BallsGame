using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _resumeButton;
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    public void DisablePauseMenu()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("LevelSelection", LoadSceneMode.Single);
    }

    private void CheckPauseMenu()
    {
        if (_playerInputActions.Player.Start.IsPressed() && _pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(_resumeButton);
        }
    }

    private void LateUpdate()
    {
        CheckPauseMenu();
    }
}
