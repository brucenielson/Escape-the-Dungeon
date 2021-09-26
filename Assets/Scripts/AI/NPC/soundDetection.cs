using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class soundDetection : MonoBehaviour
{
    private NavMeshAgent nmAgent;
    private GameObject target;
    private Animator anim;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Hero");
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        nmAgent.SetDestination(target.transform.position);

    }
}
