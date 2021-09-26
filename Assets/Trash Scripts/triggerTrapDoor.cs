using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerTrapDoor : MonoBehaviour
{
    // Start is called before the first frame update

    public bool canTriggerTrap = true;
    private GameObject trapdoor;
    public Animator anim;

    void Start()
    {
        trapdoor = GameObject.Find("TrapDoorFloor");
        anim = trapdoor.GetComponent<Animator>();
    }

    public void disableTrap()
    {
        canTriggerTrap = false;
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.CompareTag("Player") && canTriggerTrap)
            //Animator curAnimator = currentCollider.gameObject.GetComponent<Animator>().GetComponent<Pu;
            anim.SetBool("trip", true);

    }

    private void OnTriggerExit(Collider c)
    {
        
        if (c.CompareTag("Player"))
        {
            anim.SetBool("trip", false);
        }
    }
}
