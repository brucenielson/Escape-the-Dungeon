using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBoulderAudioManager : MonoBehaviour
{
    public AudioClip[] impactClips;

    private AudioSource audioSource;
    public float magnitude;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.impulse.magnitude > magnitude && audioSource != null && !audioSource.isPlaying)
        {
            PlayRandomClip(impactClips);
        }
    }

    private void PlayRandomClip(AudioClip[] clips, float volume = 1.0f)
    {
        if (!audioSource.isPlaying && clips != null && clips.Length > 0)
        {
            AudioClip clip = GetRandomClip(clips);
            audioSource.PlayOneShot(clip, volume);
        }
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
