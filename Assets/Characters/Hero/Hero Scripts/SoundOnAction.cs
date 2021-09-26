using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//John: Plays sound clip when called. Only plays once
public class SoundOnAction : MonoBehaviour
{

    //public float delay;
    public AudioClip audioClip;
    public float volume = 1.0f;
    private AudioSource audioSource;
    private bool soundPlayed = false;

    // Start is called before the first frame update
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (!soundPlayed)
        {
            soundPlayed = true;
            audioSource.PlayOneShot(audioClip, volume);
        }
    }
}
