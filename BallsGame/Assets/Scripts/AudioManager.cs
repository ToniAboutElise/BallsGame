using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _levelSongAudioSource;
    [SerializeField] private AudioSource _sfxAudioSource;

    public AudioSource GetLevelSongAudioSource() { return _levelSongAudioSource; }
    public AudioSource GetSFXAudioSource() { return _sfxAudioSource; }
}
