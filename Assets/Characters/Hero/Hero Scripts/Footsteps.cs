using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // See https://www.youtube.com/watch?v=Bnm8mzxnwP8

    [SerializeField]
    private AudioClip[] stepSounds;

    public float volume=1.0f;

    private AudioSource audioSource;
    private Animator anim;
    private float delay=0.27f;
    private float timeSince = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        timeSince += Time.deltaTime;
    }
    // Update is called once per frame
    private void Step(float impact)
    {        
        if (timeSince >= delay)
        {
            timeSince = 0f;
            float speed = Mathf.Max(Mathf.Abs(anim.GetFloat("vely")), Mathf.Abs(anim.GetFloat("strafe")));

            if (speed > 0.2f || impact > 0.0f)
            {
                AudioClip clip = GetRandomClip();
                if (impact > 0.0f)
                    audioSource.PlayOneShot(clip, impact);
                else
                    audioSource.PlayOneShot(clip, volume * speed);
            }
        }
    }

    private AudioClip GetRandomClip()
    {
        return stepSounds[UnityEngine.Random.Range(0, stepSounds.Length)];
    }
}
