using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] deathSounds;

    public float volume = 1.0f;

    private AudioSource audioSource;
    private Animator anim;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Dying(float impact)
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);

    }

    private AudioClip GetRandomClip()
    {
        return deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
    }
}

