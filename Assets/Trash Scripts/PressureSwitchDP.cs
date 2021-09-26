using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitchDP : MonoBehaviour
{

    [SerializeField]
    private Animator mySwitch;
    [SerializeField]
    private GameObject myDoor;
    public string switchType;
    private bool switchEnabled;

    public AudioClip audioClip;
    public float volume = 1.0f;
    private AudioSource audioSource;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        switchEnabled = false;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if (!switchEnabled)
            {
                switch (switchType)
                {
                    case "Ears":
                        myDoor.GetComponent<DoorPuzzle>().selectEars();
                        break;
                    case "Eyes":
                        myDoor.GetComponent<DoorPuzzle>().selectEyes();
                        break;
                    case "Fingers":
                        myDoor.GetComponent<DoorPuzzle>().selectFingers();
                        break;
                    case "Submit":
                        myDoor.GetComponent<DoorPuzzle>().submit();
                        break;
                }
                mySwitch.SetBool("PlaySwitch", true);
                playSwitchSound();
            }
            switchEnabled = true;
        } 
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
          if(switchType.Equals("Submit"))
          {
                Reset();
          }
        }
    }

    private void playSwitchSound()
    {
        audioSource.clip = audioClip;
        audioSource.loop = false;
        audioSource.Play();
    }


    public void Reset()
    {
        if(switchEnabled) {
            playSwitchSound();
        }
        mySwitch.SetBool("PlaySwitch", false);
        switchEnabled = false;
        
    }


}
