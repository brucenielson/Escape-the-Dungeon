using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointTracker : MonoBehaviour
{
    private static checkpointTracker instance;
    public Vector3 lastCheckpointPassed;
    public bool[] openedWalls;
    public bool doorsOpened;
    public Vector3 lastRotation;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else
        {
            Destroy(gameObject);
        }
    }
}
