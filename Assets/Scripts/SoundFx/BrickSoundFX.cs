using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

//John: Plays random sound on collision. TODO: random intensity. 
public class BrickSoundFX : MonoBehaviour
{

    public float magnitude; //Not used for now
    [SerializeField]
    private AudioClip[] brickSounds;
    public float volume = 1.0f;
    private AudioSource audioSource;
    private System.Random random;
    private Stopwatch stopWatch;
    private bool playSounds;

    // Start is called before the first frame update
    void Start()
    {
        playSounds = false;
        audioSource = GetComponent<AudioSource>();
        random = new System.Random();
        stopWatch = new Stopwatch();
        stopWatch.Start();

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (stopWatch.Elapsed.TotalSeconds > 5 || playSounds)
        {
            stopWatch.Stop();
            playSounds = true;
            if (collision.impulse.magnitude > 1.7)
            {
                int num = random.Next(brickSounds.Length);
                PlaySound(num);
            }
        }
    }

    private void PlaySound(int index)
    {
        AudioClip audioClip = brickSounds[index];
        if(audioClip != null )
        {
            audioSource.PlayOneShot(audioClip, volume);
        }
        
    }
}
