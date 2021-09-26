using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableTrapDoorSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject trapDoor;
    private GameObject trapDoorTriggerBox;
    private Animator trapDoorAnim;
    private Animator switchAnimator;
    private Collider currentCollider;
    public AudioClip audioClip;
    private triggerTrapDoor t;

    void Start()
    {
        switchAnimator = GetComponent<Animator>();
        trapDoor = GameObject.Find("TrapDoorFloor");
        trapDoorAnim = trapDoor.GetComponent<Animator>();
        trapDoorTriggerBox = GameObject.Find("TrapDoorTriggerBox");
        t = trapDoorTriggerBox.GetComponent<triggerTrapDoor>();

}

    // Update is called once per frame
    void Update()
    {
        if (currentCollider != null)
        {
            if (currentCollider.CompareTag("Player"))
            {
                if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                {
                    AudioSource.PlayClipAtPoint(audioClip, currentCollider.transform.position);
                    switchAnimator.SetBool("PlaySwitch", true);
                    trapDoorAnim.SetBool("trip", false);
                    t.canTriggerTrap = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        
        currentCollider = c;

    }

    private void OnTriggerExit(Collider c)
    {
        currentCollider = null;

        if (c.CompareTag("Player"))
        {

        }
    }
}
