using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string nextLevel = "0-0";
    [SerializeField] private Player _player;
    [SerializeField] private Animation _countdownAnimation;
    [SerializeField] private List<Door> _doorsList = new List<Door>();
    [SerializeField] private List<ColorSwitch> _colorSwitches = new List<ColorSwitch>();
    [SerializeField] private List<ColorSwitchAffectedGameObject> _colorSwitchAffectedGameObjects = new List<ColorSwitchAffectedGameObject>();
    [SerializeField] private AudioClip _levelSong;
    private LevelUI _levelUI;
    private AudioSource _songAudioSource;
    
    public List<ColorSwitch> GetColorSwitches() { return _colorSwitches; }
    public List<ColorSwitchAffectedGameObject> GetColorSwitchAffectedGameObjects() { return _colorSwitchAffectedGameObjects; }

    private void Awake()
    {
        _levelUI = FindObjectOfType<LevelUI>();
        _songAudioSource = FindObjectOfType<AudioManager>().GetLevelSongAudioSource();
        _songAudioSource.clip = _levelSong;
        _songAudioSource.Play();

        StartCoroutine(Countdown());

        GameManagerSingleton.GetInstance().PlaceCostumeIntoPlayer();
    }

    public void CollectableGrabbed(Collectable collectable)
    {
        collectable.SetCollected();
        _levelUI.CollectableHasBeenGrabbed();
        if (_doorsList.Count > 0)
        {
            UpdateDoorsRequiredCollectables();
        }
    }

    private IEnumerator Countdown()
    {
        float initialVelocity = _player.GetVelocity();
        _player.canRotate = false;
        _player.SetVelocity(0);
        _countdownAnimation.Play();
        yield return new WaitForSeconds(3);
        _player.SetVelocity(initialVelocity);
        _player.canRotate = true;
    }

    private void UpdateDoorsRequiredCollectables()
    {
        foreach(Door door in _doorsList)
        {
            door.UpdateRequiredCollectables();
        }   
    }
}
