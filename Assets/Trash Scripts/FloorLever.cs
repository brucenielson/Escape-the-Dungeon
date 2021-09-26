using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLever : InteractObject
{

    [SerializeField]
    private Animator mySwitch;
    [SerializeField]
    private GameObject myDoor;
    [SerializeField]
    private GameObject myDoor2;
    public AudioClip audioClip;

    //private Collider currentCollider;

    public bool disableSwitch 
    { 
        get
        {
            return !enabledObject;
        }
        set
        {
            enabledObject = !value;
        }
    }


    new public void Awake()
    {
        base.Awake();
        disableSwitch = false;
        audioSource.clip = audioClip;
    }

    

    new public void ActivateObject()
    {
        base.ActivateObject();
        if (enabledObject && !disableSwitch)
        {
            if (myDoor != null)
                myDoor.GetComponent<DoorPuzzle>().submit();
            if (myDoor2 != null)
                myDoor2.GetComponent<DoorPuzzle>().submit();
        }
    }

    //private void playSwitchSound()
    //{
    //    audioSource.clip = audioClip;
    //    PlayAudioSound();
    //}


    public void Reset()
    {
        //if (!disableSwitch)
        //{
        //    if (switchEnabled)
        //    {
        //        playSwitchSound();
        //    }
        //    mySwitch.SetBool("PlaySwitch", false);
        //    switchEnabled = false;
        //}
    }


}
