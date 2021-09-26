using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{

    public Animator myDoor;
    public GameObject switchEyes;
    public GameObject switchEars;
    public GameObject switchFingers;
    public GameObject switchSubmit;
    public bool isPuzzle; //Determines if you check puzzle is correct or just open door

    public AudioClip audioClip;
    public float volume = 1.0f;

    private AudioSource audioSource;

    private LinkedList<string> puzzleAnswer;
    private LinkedList<string> puzzleUserList;

    private bool boolEars;
    private bool boolEyes;
    private bool boolFingers;
    private bool boolDoorOpen;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        puzzleAnswer = new LinkedList<string>();
        puzzleAnswer.AddLast("Ears");
        puzzleAnswer.AddLast("Eyes");
        puzzleAnswer.AddLast("Fingers");
        puzzleUserList = new LinkedList<string>();
        boolEars = false;
        boolEyes = false;
        boolFingers = false;
        boolDoorOpen = false;
    }


    public void selectFingers()
    {
        if(!boolFingers)
        {
            puzzleUserList.AddLast("Fingers");
            boolFingers = true;
        }
    }

    public void selectEyes()
    {
        if (!boolEyes)
        {
            puzzleUserList.AddLast("Eyes");
            boolEyes = true;
        }
    }

    public void selectEars()
    {
        if (!boolEars)
        {
            puzzleUserList.AddLast("Ears");
            boolEars = true;
        }
    }

    public void submit()
    {
        //If it's a puzzle validate the puzzle is correct
        if (isPuzzle)
        {
            if (boolEars && boolEyes && boolFingers)
            {
                if (puzzleAnswer.First.Value.Equals(puzzleUserList.First.Value) && puzzleAnswer.Last.Value.Equals(puzzleUserList.Last.Value))
                {
                    //Open Door
                    OpenCell();

                }
                else
                {
                    Reset();
                }
            }
            else
            {
                Reset();
            }
        }
        //It's not a puzzle just open the door
        else
        {
            OpenCell();
        }
    }

    private void OpenCell()
    {
        myDoor.SetBool("PlayDoor", true);
        playCellSound();
        boolDoorOpen = true;
        switchSubmit.GetComponent<FloorLever>().disableSwitch = true;
    }

    private void Reset()
    {
        boolEars = false;
        boolEyes = false;
        boolFingers = false;
        puzzleUserList = new LinkedList<string>();
        switchSubmit.GetComponent<FloorLever>().Reset();
        switchEars.GetComponent<PressureSwitchDP>().Reset();
        switchEyes.GetComponent<PressureSwitchDP>().Reset();
        switchFingers.GetComponent<PressureSwitchDP>().Reset();
    }

    private void playCellSound()
    {
        if (!boolDoorOpen && audioClip != null && audioSource != null)
        {
            //AudioSource audioS = switchEars.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

}
