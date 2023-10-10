using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource SFXSource;
    public PlayAudioEventSO SFXAudioEvent;
    public PlayAudioEventSO BGMAudioEvent;

    private void OnEnable() {
        SFXAudioEvent.OnEventRaised += PlayAudio;
        BGMAudioEvent.OnEventRaised += PlayBGMAudio;
    }

    private void OnDisable() {
        SFXAudioEvent.OnEventRaised -= PlayAudio;
        BGMAudioEvent.OnEventRaised -= PlayBGMAudio;
    }

    private void PlayAudio(AudioClip clip) {
        SFXSource.clip = clip;
        SFXSource.Play();
    }

    private void PlayBGMAudio(AudioClip clip) {
        BGMSource.clip = clip;
        BGMSource.Play();
    }
}
