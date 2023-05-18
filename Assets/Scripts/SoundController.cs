using System;
using UnityEngine;

public class SoundController : MonoBehaviour {
    
    private AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayPauseSound() {
        if (_audioSource.isPlaying) {
            _audioSource.Pause();
        } else {
            _audioSource.UnPause();
        }
    }

}