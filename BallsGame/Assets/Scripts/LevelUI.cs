using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private Button _quitButton;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _quitButton.onClick.AddListener(QuitButtonExtraFunctions);
    }    

    public void DisablePauseMenu()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("LevelSelection", LoadSceneMode.Single);
        Time.timeScale = 1;
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

    private void QuitButtonExtraFunctions()
    {
        FindObjectOfType<AudioManager>().GetLevelSongAudioSource().Stop();
    }

    private void LateUpdate()
    {
        CheckPauseMenu();
    }
}
