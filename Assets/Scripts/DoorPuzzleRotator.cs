using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzleRotator : MonoBehaviour
{

    public Animator myDoor;

    public AudioClip audioClip;
    public float volume = 1.0f;

    private AudioSource audioSource;
    private bool boolDoorOpen;



    public bool boolPuzzle1 { get; set; }
    public bool boolPuzzle2 { get; set; }
    public bool boolPuzzle3 { get; set; }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boolPuzzle1 = false;
        boolPuzzle2 = false;
        boolPuzzle3 = false;
        boolDoorOpen = false;
    }


    public void submit(string answerkey, bool state)
    {
        switch (answerkey)
        {
            case "Puzzle1":
                boolPuzzle1 = state;
                break;
            case "Puzzle2":
                boolPuzzle2 = state;
                break;
            case "Puzzle3":
                boolPuzzle3 = state;
                break;
        }

        if (boolPuzzle1 && boolPuzzle2 && boolPuzzle3)
        {
            //Open Door
            OpenCell();
        }
    }

    private void OpenCell()
    {
        myDoor.SetBool("PlayDoor", true);
        playCellSound();
        boolDoorOpen = true;
    }

    private void Reset()
    {

    }

    private void playCellSound()
    {
        if (!boolDoorOpen)
        {
            //AudioSource audioS = switchEars.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

}
