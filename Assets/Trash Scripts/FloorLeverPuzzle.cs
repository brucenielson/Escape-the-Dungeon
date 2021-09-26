using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FloorLeverPuzzle : InteractObject
{

    [SerializeField]
    private Animator mySwitch;
    [SerializeField]
    private Animator myRotator;
    [SerializeField]
    private GameObject myDoor;
    public int answer;
    public string answerkey;
    
    public AudioClip audioClip;
//    public float volume;
 //   private AudioSource audioSource;
    private int rotation;

    //public float timer = 1000f;
    //private bool leverPulled;
//    private Collider currentCollider;


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

    //private void Start()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //    switchEnabled = false;
    //    disableSwitch = false;
    //    rotation = 0;
    //    //coroutine = ResetLever();
    //    //cinput = GetComponent<CharacterInputController>();
    //    //if (cinput == null)
    //    //    Debug.Log("CharacterInput could not be found");
    //}

    //private void Awake()
    //{
    //    cinput = GetComponent<CharacterInputController>();
    //    if (cinput == null)
    //        Debug.Log("CharacterInput could not be found");
    //}

    //private void Update()
    //{

    //    //if (currentCollider != null)
    //    //{
    //    //    if(currentCollider.CompareTag("Player"))
    //    //    //Animator curAnimator = currentCollider.gameObject.GetComponent<Animator>().GetComponent<Pu;
    //    //    PuzzleAction(myRotator);
    //    //}
    //}

    new public void ActivateObject()
    {
        base.ActivateObject();
        if (enabledObject && !disableSwitch)
        {
            //Debug.Log("Button Clicked");
            //leverPulled = true;
            //mySwitch.SetBool("PlaySwitch", true);
            //Debug.Log("Lever True");

            //StartCoroutine("ResetLever");
            //playSwitchSound();
            if (rotation == 4)
            {
                rotation = 0;
            }
            rotation++;

            //myRotator.SetInteger("RotationNumber", rotation);
            myRotator.SetInteger("RotationNumber", rotation);

            if (rotation == answer)
            {
                myDoor.GetComponent<DoorPuzzleRotator>().submit(answerkey, true);
            }
            else
            {
                myDoor.GetComponent<DoorPuzzleRotator>().submit(answerkey, false);
            }
        }
    }
    //private void OnTriggerStay(Collider c)
    //{
    //    //currentCollider = c;
    //    PuzzleAction(myRotator);
    //    //if (Input.GetMouseButtonUp(0) || Input.GetButtonDown("Fire1")) {
    //    //    Reset();
    //    //}
    //    //delayReset();
    //}

    //private void OnTriggerEnter(Collider c)
    //{
    //    currentCollider = c;
    //}

    //private void OnTriggerExit(Collider c)
    //{
    //    currentCollider = null;
    //    if (c.CompareTag("Player"))
    //    {
    //        Reset();
    //    }
    //}

    //private void PuzzleAction(Animator myAnimator)
    //{
    //    if (currentCollider.CompareTag("Player"))
    //    {
    //        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
    //        {
    //            Debug.Log("Button Clicked");
    //            //leverPulled = true;
    //            mySwitch.SetBool("PlaySwitch", true);
    //            //Debug.Log("Lever True");

    //            StartCoroutine("ResetLever");
    //            playSwitchSound();
    //            if (rotation == 4)
    //            {
    //                rotation = 0;
    //            }
    //            rotation++;

    //            //myRotator.SetInteger("RotationNumber", rotation);
    //            myAnimator.SetInteger("RotationNumber", rotation);

    //            if (rotation == answer)
    //            {
    //                myDoor.GetComponent<DoorPuzzleRotator>().submit(answerkey, true);
    //            }
    //            else
    //            {
    //                myDoor.GetComponent<DoorPuzzleRotator>().submit(answerkey, false);
    //            }
    //        }
    //    }
    //}




    //private void playSwitchSound()
    //{
    //    audioSource.clip = audioClip;
    //    audioSource.volume = volume;
    //    audioSource.loop = false;
    //    audioSource.Play();
    //}

    //public void Reset()
    //{
    //    mySwitch.SetBool("PlaySwitch", false);
    //    //if (!disableSwitch)
    //    //{
    //    //    if (switchEnabled)
    //    //    {
    //    //        playSwitchSound();
    //    //    }
    //    //    mySwitch.SetBool("PlaySwitch", false);
    //    //    switchEnabled = false;
    //    //}
    //}

    //private IEnumerator ResetLever()
    //{
    //    yield return new WaitForSeconds(0.7f);
    //    //leverPulled = false;
    //    mySwitch.SetBool("PlaySwitch", false);
    //    //Debug.Log("Lever false");
    //}
}
