using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TMP_Text _ballsLeftText;
    [SerializeField] private TMP_Text _slashText;
    [SerializeField] private TMP_Text _totalBalls;
    private int collectablesAmount = 0;
    private int collectablesGrabbed = 0;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _quitButton.onClick.AddListener(QuitButtonExtraFunctions);

        foreach (Collectable collectable in FindObjectsOfType<Collectable>())
        {
            if (collectable.GetCollectableState() == Collectable.CollectableState.NonCollected || collectable.GetCollectableState() == Collectable.CollectableState.ProtectedByAdditionalEffect)
                collectablesAmount++;
        }

        Debug.Log(collectablesAmount);
        _totalBalls.text = collectablesAmount.ToString();
        _ballsLeftText.text = collectablesGrabbed.ToString();
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

    public void CollectableHasBeenGrabbed()
    {
        collectablesGrabbed++;
        _ballsLeftText.text = collectablesGrabbed.ToString();

        if(collectablesGrabbed == collectablesAmount)
        {
            _ballsLeftText.color = Color.yellow;
            _slashText.color = Color.yellow;
            _totalBalls.color = Color.yellow;
        }
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
