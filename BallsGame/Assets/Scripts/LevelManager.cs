using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string nextLevel = "0-0";
    [SerializeField] private Player _player;
    [SerializeField] private Animation _countdownAnimation;
    [SerializeField] private Text _ballsLeftText;
    [SerializeField] private List<Door> _doorsList = new List<Door>();
    [SerializeField] private List<ColorSwitch> _colorSwitches = new List<ColorSwitch>();
    [SerializeField] private List<ColorSwitchAffectedGameObject> _colorSwitchAffectedGameObjects = new List<ColorSwitchAffectedGameObject>();
    [SerializeField] private AudioClip _levelSong;
    private AudioSource _songAudioSource;
    private int collectablesAmount = 0;
    private int collectablesGrabbed = 0;
    
    public List<ColorSwitch> GetColorSwitches() { return _colorSwitches; }
    public List<ColorSwitchAffectedGameObject> GetColorSwitchAffectedGameObjects() { return _colorSwitchAffectedGameObjects; }

    private void Awake()
    {
        _songAudioSource = FindObjectOfType<AudioManager>().GetLevelSongAudioSource();
        _songAudioSource.clip = _levelSong;
        _songAudioSource.Play();

        StartCoroutine(Countdown());

        foreach(Collectable collectable in FindObjectsOfType<Collectable>())
        {
            if(collectable.GetCollectableState() == Collectable.CollectableState.NonCollected || collectable.GetCollectableState() == Collectable.CollectableState.ProtectedByAdditionalEffect)
            collectablesAmount++;
        }

        GameManagerSingleton.GetInstance().PlaceCostumeIntoPlayer();

        Debug.Log(collectablesAmount);
        _ballsLeftText.text = collectablesAmount.ToString();
    }

    public void CollectableGrabbed(Collectable collectable)
    {
        collectable.SetCollected();
        collectablesAmount--;
        collectablesGrabbed++;
        _ballsLeftText.text = collectablesAmount.ToString();
        if(_doorsList.Count > 0)
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
