using System.Collections;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Assets.Scripts.Helpers;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerDetector : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rbody;
    private GameObject player;
    private SphereCollider detectionCollider;
    public NavMeshAgent navMeshAgent;
    private bool playerDetected;
    private bool playerInRange;
    private float detectionAnimationTime;
    private DateTime? playerDetectedAt;

    public float turnSpeed;
    public float sightDetectionRange;
    public float hearingDetectionRange;
    public float lineOfSightAngle;
    public string detectionAnimationName;

    private DetectionStates state;

    public bool useWaypoints;
    public GameObject[] waypoints;

    public int currWaypoint = -1;
    private GameObject sphere;

    public string objectTag;
    private bool isDead;
    private bool dyingTriggered;
    private ZombieAudioManager audioManager;

    private DateTime timeForNextGroan;

    public float distanceToAttack;
    public float distanceToKill;

    private bool showWaypoint = false;

    void Awake()
    {
        detectionCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        detectionCollider.radius = sightDetectionRange > hearingDetectionRange ? sightDetectionRange : hearingDetectionRange;
        detectionCollider.isTrigger = true;
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();

        audioManager = GetComponent<ZombieAudioManager>();

        detectionAnimationTime = AnimationHelpers.GetAnimationTime(animator, detectionAnimationName);

        ChangeState();

        timeForNextGroan = DateTime.Now.AddSeconds(RandomNumberHelpers.GetRandomNumber(1f, 6f));

        if (showWaypoint)
        {
            // create a sphere that will be used to visualize the navmesh destination
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            DestroyImmediate(sphere.GetComponent<Collider>());
        }
    }

    private void ChangeState()
    {
        if (isDead)
        {
            SwitchToDeadState();;
            state = DetectionStates.Dead;
        }
        else if (playerDetected)
        {
            SwitchToAggresiveState();
        }
        else if (playerInRange)
        {
            SwitchToAlertState();
        }
        else if (useWaypoints && waypoints != null && waypoints.Length > 0)
        {
            SwitchToPatrolState();
        }
        else
        {
            SwitchToIdleState();
        }
    }

    private void SwitchToDeadState()
    {
        SetPlayerNotDetected();
        state = DetectionStates.Dead;
    }

    private void SwitchToAlertState()
    {
        if (state != DetectionStates.Alert)
        {
            state = DetectionStates.Alert;
            animator.SetBool("aggresive", false);
        }
    }

    private void SwitchToPatrolState()
    {
        animator.SetBool("walking", true);
        if (state != DetectionStates.Patrol)
        {
            state = DetectionStates.Patrol;
            SetPlayerNotDetected();
        }
    }

    private void SwitchToIdleState()
    {
        if (state != DetectionStates.Idle)
        {
            state = DetectionStates.Idle;
            SetPlayerNotDetected();
        }
    }

    private void SwitchToAggresiveState()
    {
        if (state != DetectionStates.Aggresive)
        {
            state = DetectionStates.Aggresive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectIfPlayerIsVisible();
        ChangeState();

        switch (state)
        {
            case DetectionStates.Aggresive:
                Aggresive();
                break;
            case DetectionStates.Alert:
                Alert();
                break;
            case DetectionStates.Patrol:
                Patrol();
                break;
            case DetectionStates.Idle:
                Idle();
                break;
            case DetectionStates.Dead:
                Die();
                break;
        }
    }

    private void Aggresive()
    {
        AnimateDetectionAndChasePlayer();
        AttackIfInRange();
    }

    private void AttackIfInRange()
    {
        if (player != null)
        {
            var distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance <= distanceToKill)
            {
                var playerAnimator = player.GetComponent<Animator>();
                playerAnimator.SetBool("isDead", true);
                StartCoroutine(WaitForSceneLoad());
                audioManager.Eat();
            }
            if (distance <= distanceToAttack)
            {
                animator.SetBool("attacking", true);
            }
            else
            {
                animator.SetBool("attacking", false);
            }
        }
        else
        {
            animator.SetBool("attacking", false);
        }
    }

    private void Alert()
    {
        
    }

    private void Die()
    {
        if (!dyingTriggered)
        {
            audioManager.DeathCry();
            animator.SetBool("dead", true);
            this.navMeshAgent.isStopped = true;
            Destroy(this.gameObject, 3f);
            dyingTriggered = true;
        }
    }

    private void Patrol()
    {
        Groan();

        CheckCurrentWaypoint();

        GoToWayPoint(waypoints[currWaypoint].transform.position);
    }

    private void Idle()
    {
        this.navMeshAgent.isStopped = true;
        animator.SetBool("walking", false);
        animator.SetBool("aggresive", false);
        Groan();
    }

    private void Groan()
    {
        if (timeForNextGroan <= DateTime.Now)
        {
            audioManager.Groan();
            timeForNextGroan = DateTime.Now.AddSeconds(RandomNumberHelpers.GetRandomNumber(5f, 15f));
        }
    }

    private void GoToWayPoint(Vector3 target)
    {
        this.navMeshAgent.isStopped = false;

        this.navMeshAgent.SetDestination(target);

        if (showWaypoint)
        {
            sphere.transform.position = target;
        }
    }

    private void CheckCurrentWaypoint()
    {
        if (currWaypoint < 0 || currWaypoint >= waypoints.Length)
        {
            currWaypoint = 0;
        }
        else if (navMeshAgent.remainingDistance < 0.2f && !navMeshAgent.pathPending)
        {
            currWaypoint = (currWaypoint + 1) % waypoints.Length;
        }
    }

    private void AnimateDetectionAndChasePlayer()
    {
        if (playerDetectedAt != null && playerDetectedAt.Value.AddSeconds(detectionAnimationTime) <= DateTime.Now)
        {
            animator.SetBool("aggresive", true);
            ChasePlayer();
        }
        else
        {
            TurnTowardsPlayer();
        }
    }
    
    private void ChasePlayer()
    {
        var predictedPosition = GetPredictedPlayerLocation();

        GoToWayPoint(predictedPosition);
    }

    private Vector3 GetPredictedPlayerLocation()
    {
        var velocityReporter = player.GetComponent<VelocityReporter>();
        if (velocityReporter != null && velocityReporter.velocity.magnitude > 0.5)
        {
            bool locationPredicted = false;
            var timeToPlayer = Vector3.Distance(player.transform.position, this.transform.position) /
                               navMeshAgent.speed;
            while (!locationPredicted && timeToPlayer > 0.1)
            {
                var newLocation = player.transform.position + (velocityReporter.velocity * timeToPlayer);
                NavMeshHit hit;
                locationPredicted = NavMesh.SamplePosition(newLocation, out hit, 1.0f, NavMesh.AllAreas);

                if (locationPredicted)
                {
                    return hit.position;
                }
                else
                {
                    timeToPlayer /= 2;
                }
            }
        }
        return player.transform.position;
    }

    private void TurnTowardsPlayer()
    {
        // This rotates over time based on speed setting
        // From: https://answers.unity.com/questions/862380/how-to-slow-down-transformlookat.html
        // See also: https://answers.unity.com/questions/1713658/slowly-rotate-to-look-at.html
        Vector3 relativePos = player.transform.position - transform.position;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            player = c.gameObject;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            player = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == objectTag)
        {
            isDead = true;
        }
    }

    private void DetectIfPlayerIsVisible()
    {
        if (player != null)
        {
            playerInRange = true;
            RaycastHit raycastHit;
            bool lineCastHitsPlayer = Physics.Linecast(transform.position, player.transform.position, out raycastHit);

            if (lineCastHitsPlayer)
            {
                if (raycastHit.distance <= hearingDetectionRange)
                {
                    SetPlayerDetected();
                }
                else
                {
                    // 180 degrees is directly in front of npc while 0 degrees is directly behind 
                    float angleBetweenForwardAndLine = Vector3.Angle(raycastHit.normal, this.transform.forward);

                    if (angleBetweenForwardAndLine >= (180 - lineOfSightAngle))
                    {
                        SetPlayerDetected();
                    }
                }
            }
            else
            {
                SetPlayerNotDetected();
            }
        }
        else
        {
            playerInRange = false;
            playerDetected = false;
        }
    }

    private void SetPlayerDetected()
    {
        if (state != DetectionStates.Aggresive)
        {
            playerDetected = true;
            animator.SetBool("playerDetected", true);
            animator.SetBool("walking", false);
            playerDetectedAt = DateTime.Now;
        }
    }

    private void SetPlayerNotDetected()
    {
        playerDetected = false;
        playerDetectedAt = null;
        animator.SetBool("playerDetected", false);
        animator.SetBool("walking", false);
        animator.SetBool("aggresive", false);
        animator.SetBool("attacking", false);
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnAnimatorMove()
    {
        try
        {
            var newSpeed = (animator.deltaPosition / Time.deltaTime).magnitude;
            if (float.IsNaN(newSpeed) || float.IsInfinity(newSpeed))
            {
                navMeshAgent.speed = 0;
            }
            else
            {
                navMeshAgent.speed = newSpeed;
            }
        }
        catch (Exception e)
        {
            navMeshAgent.speed = 0;
            UnityEngine.Debug.Log(e.Message);
        }
    }

    enum DetectionStates
    {
        Idle,
        Alert,
        Patrol,
        Aggresive,
        Dead
    }
}
 