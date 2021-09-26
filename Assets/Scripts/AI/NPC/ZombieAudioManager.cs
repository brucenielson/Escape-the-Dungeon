using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class ZombieAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] fallClips;
    [SerializeField] private AudioClip[] stepClips;
    [SerializeField] private AudioClip[] idleClips;
    [SerializeField] private AudioClip[] deathCryClips;
    [SerializeField] private AudioClip[] screamClips;
    [SerializeField] private AudioClip[] eatClips;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        PlayRandomClip(stepClips, 0.50f);
    }
    public void Fall()
    {
        PlayRandomClip(fallClips);
    }
    public void Groan()
    {
        PlayRandomClip(idleClips, 0.5f);
    }

    public void DeathCry()
    {
        PlayRandomClipWithInterrupt(deathCryClips);
    }

    public void Scream()
    {
        PlayRandomClipWithInterrupt(screamClips);
    }

    public void Eat()
    {
        PlayRandomClip(eatClips, 0.5f);
    }

    private void PlayRandomClip(AudioClip[] clips, float volume = 1.0f)
    {
        if (!audioSource.isPlaying && clips != null && clips.Length > 0)
        {
            AudioClip clip = GetRandomClip(clips);
            audioSource.PlayOneShot(clip, volume);
        }
    }

    private void PlayRandomClipWithInterrupt(AudioClip[] clips, float volume = 1.0f)
    {
        if (clips != null && clips.Length > 0)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            AudioClip clip = GetRandomClip(clips);
            audioSource.PlayOneShot(clip, volume);
        }
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
