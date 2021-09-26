using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;

public class CeilingTrapTrigger : MonoBehaviour
{
    public GameObject trap;
    public GameObject trapPrefab;
    private TrapState state;
    private Animator animator;
    private Rigidbody trapRigidbody;
    private DateTime? trapFellAt;
    private AudioSource audioSource;
    private Vector3 originalPosition;

    private void Awake()
    {
        if (trap != null)
        {
            trapRigidbody = trap.GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            originalPosition =
                new Vector3(trapRigidbody.position.x, trapRigidbody.position.y, trapRigidbody.position.z);
        }

        state = TrapState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TrapState.Idle:
                break;
            case TrapState.Falling:
                HandleFalling();
                break;
            case TrapState.Triggered:
                HandleTriggered();
                break;
            case TrapState.Reseting:
                HandleResetting();
                break;
        }
    }

    private void HandleResetting()
    {
        if (trap != null)
        {
            Destroy(trap);
        }
        trap = Instantiate(trapPrefab, originalPosition, Quaternion.identity);
        trapRigidbody = trap.GetComponent<Rigidbody>();
        trapRigidbody.useGravity = false;
        trapFellAt = null;
        state = TrapState.Idle;
    }

    private void HandleTriggered()
    {
        trapRigidbody.useGravity = true;
        trapFellAt = DateTime.Now;
        state = TrapState.Falling;
    }

    private void HandleFalling()
    {
        if (trapFellAt != null && trapFellAt.Value <= DateTime.Now.AddSeconds(-5))
        {
            state = TrapState.Reseting;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (Vector3.Distance(other.transform.position, transform.position) < 1.0)
            {
                animator.SetBool("triggered", true);
                if (state == TrapState.Idle)
                {
                    state = TrapState.Triggered;
                }
            }
            else
            {
                animator.SetBool("triggered", false);
            }
        }
    }
    
     public void PlaySound()
     {
         audioSource.Play();
    }

    public enum TrapState
    {
        Idle,
        Triggered,
        Falling,
        Reseting
    }

}
