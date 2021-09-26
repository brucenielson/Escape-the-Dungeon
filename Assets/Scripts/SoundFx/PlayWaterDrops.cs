using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWaterDrops : MonoBehaviour
{

    
    public AudioClip audioClip;
    public float volume = 1.0f;
    private AudioSource audioSource;
    private bool playedSound = false;

    // Start is called before the first frame update
    void Start()
    {
 
       
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        if(!playedSound)
        {
            audioSource = GetComponent<AudioSource>();
            //audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.loop = false;
            audioSource.PlayOneShot(audioClip);
            playedSound = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
