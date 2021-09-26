using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room01PerformanceControl : MonoBehaviour
{
    public GameObject[] muteUntilInRoom;
    public GameObject[] disableUntilInRoom;
    public GameObject[] muteForSecondsAfterLoading;
    private DateTime sceneLoadedAt;
    private bool unMutedAfterWaiting;
    private bool unMutedAfterEntering;
    private int timeToWait;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoadedAt = DateTime.Now;

        if (muteUntilInRoom.Length > 0 || disableUntilInRoom.Length > 0)
        {
            DisableAll(disableUntilInRoom);
            MuteAll(muteUntilInRoom);
            unMutedAfterEntering = false;
        }
        else
        {
            unMutedAfterEntering = true;
        }

        if (muteForSecondsAfterLoading != null && muteForSecondsAfterLoading.Length > 0)
        {
            MuteAll(muteForSecondsAfterLoading);
            unMutedAfterWaiting = false;
            timeToWait = 3;
        }
        else
        {
            unMutedAfterWaiting = true;
        }
    }

    void Update()
    {
        if (!unMutedAfterWaiting && DateTime.Now >= sceneLoadedAt.AddSeconds(timeToWait))
        {
             UnMuteAll(muteForSecondsAfterLoading);
             unMutedAfterWaiting = true;
        }
    }

    private void MuteAll(GameObject[] gameObjects)
    {
        foreach (var go in gameObjects)
        {
            var audioSources = go.GetComponentsInChildren<AudioSource>();

            if (audioSources != null && audioSources.Length > 0)
            {
                foreach (var audioSource in audioSources)
                {
                    audioSource.mute = true;
                }
            }
        }
    }

    private void UnMuteAll(GameObject[] gameObjects)
    {
        foreach (var go in gameObjects)
        {
            var audioSources = go.GetComponentsInChildren<AudioSource>();

            if (audioSources != null && audioSources.Length > 0)
            {
                foreach (var audioSource in audioSources)
                {
                    audioSource.mute = false;
                }
            }
        }
    }
    private void DisableAll(GameObject[] gameObjects)
    {
        foreach (var go in gameObjects)
        {
            go.SetActive(false);
        }
    }

    private void EnableAll(GameObject[] gameObjects)
    {
        foreach (var go in gameObjects)
        {
            go.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (unMutedAfterEntering == false && c.gameObject.tag == "Player")
        {
            EnableAll(disableUntilInRoom);
            UnMuteAll(muteUntilInRoom);
            unMutedAfterEntering = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            MuteAll(muteUntilInRoom);
            DisableAll(disableUntilInRoom);
            unMutedAfterEntering = false;
        }
    }
}
