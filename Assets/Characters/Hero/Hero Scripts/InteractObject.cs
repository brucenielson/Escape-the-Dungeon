using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    private Animator anim;
    public float volume = 1.0f;
    protected AudioSource audioSource;
    public bool autoReset;
    public bool enabledObject;
    protected bool switchActivated;


    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        switchActivated = false;
    }

    protected void PlayAudioSound()
    {
        audioSource.loop = false;
        audioSource.Play();
    }

    public void ActivateObject()
    {
        if (enabledObject)
        {
            enabledObject = true;
            anim.SetBool("PlaySwitch", true);
            PlayAudioSound();
        }
    }


    public void OnAnimatorMove()
    {
        AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);
        if (autoReset && (astate.IsName("Activate") || astate.IsName("Deactive") || anim.IsInTransition(0)))
        {
            anim.SetBool("PlaySwitch", false);
        }
    }
}

