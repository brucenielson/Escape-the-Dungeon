using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip jumpSoundExtreme;
    //private AudioClip[] jumpSound;

    public float volume = 0.8f;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider c)
    {
        //if (c.CompareTag("JumpSound"))
        //{
        //    if (Input.GetKeyDown("space") || Input.GetButtonDown("Jump"))
        //    {
        //        audioSource.clip = jumpSound;
        //        audioSource.volume = volume;
        //        audioSource.loop = false;
        //        audioSource.PlayDelayed(0.1f);
        //    }
                
        //}

        //if (Input.GetKeyDown("space") || Input.GetButtonDown("Jump"))
        //{
            
        //}
    }


    private void Update() 
    {
        if (Input.GetKeyDown("space") || Input.GetButtonDown("Jump"))
        {
            audioSource.clip = jumpSound;
            audioSource.volume = 1.0f;
            audioSource.loop = false;
            audioSource.PlayDelayed(0.1f);
        }
    }

}

