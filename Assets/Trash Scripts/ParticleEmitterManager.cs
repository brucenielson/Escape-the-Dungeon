using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

public class ParticleEmitterManager : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private ParticleState state;

    private DateTime timeForNextEmission;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        state = ParticleState.Idle;
        timeForNextEmission = DateTime.Now.AddSeconds(RandomNumberHelpers.GetRandomNumber(0, 10));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case ParticleState.Idle:
                HandleIdle();
                break;
            case ParticleState.Emitting:
                HandleEmitting();
                break;
        }
    }

    private void HandleEmitting()
    {
        if (!_particleSystem.isPlaying)
        {
            _particleSystem.Stop();
            state = ParticleState.Idle;
            timeForNextEmission = DateTime.Now.AddSeconds(RandomNumberHelpers.GetRandomNumber(0, 10));
        }
    }

    private void HandleIdle()
    {
        if (timeForNextEmission <= DateTime.Now)
        {
            state = ParticleState.Emitting;
            _particleSystem.Play();
        }
    }

    enum ParticleState
    {
        Idle,
        Emitting
    }
}
