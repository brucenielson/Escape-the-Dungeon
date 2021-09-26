using System.Collections;
using UnityEngine;
using System;
using Assets.Scripts.Helpers;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MenuZombie : MonoBehaviour
{
    private Animator animator;

    private MenuStates state;

    private ZombieAudioManager audioManager;
    private DateTime timeForNextGroan;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("walking", true);
        state = MenuStates.Idle;
        timeForNextGroan = DateTime.Now.AddSeconds(RandomNumberHelpers.GetRandomNumber(5f, 10f));
        audioManager = GetComponent<ZombieAudioManager>();
    }

    void Update()
    {

        switch (state)
        {
            case MenuStates.Aggresive:
                Aggresive();
                break;
            case MenuStates.Idle:
                Idle();
                break;
        }
    }

    private void Aggresive()
    {
        animator.SetBool("aggresive", true);
    }
    
    private void Idle()
    {
        Groan();
    }

    private void Groan()
    {
        if (timeForNextGroan <= DateTime.Now)
        {
            audioManager.Groan();
            timeForNextGroan = DateTime.Now.AddSeconds(RandomNumberHelpers.GetRandomNumber(5f, 20f));
        }
    }

    void OnAnimatorMove()
    {
        
    }

    enum MenuStates
    {
        Idle,
        Aggresive
    }
}
