using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFXTimer : MonoBehaviour
{

    public float delay;
    public AudioClip audioClip;
    public float volume = 1.0f;
    private AudioSource audioSource;


    public void PlaySoundTimer()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = false;
        audioSource.PlayDelayed(delay);
    }
}


