using Assets.Scripts.Helpers;
using UnityEngine;
using TMPro;

// Jumping animation example: https://www.youtube.com/watch?v=6Zg2Hgwg7OM

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class HeroController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    // Camera variables
    private Vector3 originalCameraPos;
    private Vector3 offset = new Vector3(0f, 1.8f, 0f);
    private Vector3 startPos;
    private float distToCamera;
    private Vector3 dirToCamera;
    private GameObject currentCamera;

    // IK Animation variables
    private bool doingIKAnim = false;
    private Vector3 saveTargetPos;

    public float animationSpeed = 1.0f;
    public float turnMaxSpeed = 60.0f;
    public float jumpHeightMod = 1f;
    public float jumpForwardMod = 1f;

    //public float rootMovementSpeed = 1.0f;
    //public float rootTurnSpeed = 1.0f;
    private float inputForward = 0f;
    private float inputTurn = 0f;
    private bool inputJump = false;
    private float startJump = 0f;
    private bool inputAction = false;
    private float inputMouseX = 0f;
    private float inputMouseY = 0f;
    private float inputStrafe = 0f;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private Quaternion startCameraRotation;
    private float origSizeCollider;
    private Vector3 origCenterCollider;
    private CapsuleCollider heroCollider;
    private Vector3 hitLocation;
    private float shrunkHeight = 13f;
    private SkinnedMeshRenderer mesh;

    //Useful if you implement jump in the future...
    public float jumpableGroundNormalMaxAngle = 45f;

    public GameObject interactionObject { get; set; }

    void Awake()
    {
        originalCameraPos = this.transform.Find("Main Camera").localPosition;
        currentCamera = this.transform.Find("Main Camera").gameObject;
        startPos = this.transform.position + offset;
        dirToCamera = this.transform.TransformPoint(originalCameraPos) - startPos;
        distToCamera = dirToCamera.magnitude;
        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();
        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");

        anim.SetBool("grounded", true);
        anim.SetBool("jump", false);
        startJump = -1f;
        startCameraRotation = currentCamera.transform.localRotation;
        heroCollider = GetComponent<CapsuleCollider>();
        origSizeCollider = heroCollider.height;
        origCenterCollider = heroCollider.center;

        // Grab mesh renderer
        Transform child = transform.Find("Dreyar");
        mesh = child.GetComponent<SkinnedMeshRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        // Get input from CharacterInputController
        if (cinput.enabled)
        {
            inputForward = cinput.Forward;
            inputTurn = cinput.Turn;
            inputAction = inputAction | cinput.Action;
            inputJump = inputJump | cinput.Jump;
            inputMouseX = cinput.MouseYaw;
            inputMouseY = cinput.MousePitch;
            inputStrafe = cinput.Strafe;
        }

        // Example of how to adjust animation speed programatically. 1.0 = normal animation speed.


        if (inputForward >= 0)
            anim.speed = animationSpeed;


        // Play animations
        anim.SetFloat("vely", inputForward);
        anim.SetFloat("strafe", inputStrafe);

        HoldItem itemHolder = GetComponent<HoldItem>();
        MatchTargetForAnimation("Pickup Object", itemHolder.nearItem, 0.657f, 0.6f);
        MatchTargetForAnimation("Grab Object", itemHolder.nearItem, 0.538f, 1.25f);
        MatchTargetForAnimation("Place Tablet", interactionObject, 0.538f, 2.0f);

    }


    private void MatchTargetForAnimation(string animationStateName, GameObject target, float touchTime, float reach)
    {
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
        if (animState.IsName(animationStateName)  && target != null)
        {

            if (!anim.IsInTransition(0) && !anim.isMatchingTarget)
            {
                Vector3 targetDir = target.transform.position - transform.position;
                targetDir.y = 0f;
                startRotation = transform.rotation;
                Quaternion rotationAmt = Quaternion.LookRotation(targetDir);
                // We don't really need to rotate all the way due to IK so allow a bit of a buffer
                float itemAngleDegrees = Quaternion.Angle(startRotation, rotationAmt);
                float adjustRotationBy = 60f;
                if (itemAngleDegrees > adjustRotationBy)
                {
                    float adjustedRotation = (itemAngleDegrees - adjustRotationBy) / itemAngleDegrees;
                    targetRotation = Quaternion.Lerp(startRotation, rotationAmt, adjustedRotation);
                }
                else
                    targetRotation = startRotation;

                float dist = Vector3.Distance(target.transform.position, transform.position);
                Vector3 targetPos;
                if (dist > reach)
                {
                    float ratio = (dist - reach) / dist;
                    targetPos = (targetDir * ratio) + transform.position;
                }
                else
                {
                    targetPos = transform.position;
                }
                //transform.rotation = targetRot;
                //transform.position = targetPos;

                anim.MatchTarget(targetPos, targetRotation,
                    AvatarTarget.Root,
                    new MatchTargetWeightMask(Vector3.one, 0f), 0f, touchTime);

            }
            else if (anim.isMatchingTarget)
            {
                // MatchTarget doesn't seem to work on rotations, so I implemented this instead
                // This rotates over time based on speed setting
                // From: https://answers.unity.com/questions/862380/how-to-slow-down-transformlookat.html
                // See also: https://answers.unity.com/questions/1713658/slowly-rotate-to-look-at.html
                float time = animState.normalizedTime;
                float t = time / touchTime;
                // Now do actual rotation
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            }
        }
    }


    private void FixedUpdate()
    {
        mesh.enabled = true;
        // Cast a ray from player to camera to see if there is something blocking camera
        startPos = transform.position + offset;
        dirToCamera = transform.TransformPoint(originalCameraPos) - startPos;
        Ray ray = new Ray(startPos, dirToCamera);
        RaycastHit hit;
        int layerMask = ~(1 << 9 | 1 << 12 | 1 << 13);
        bool isBlocked = Physics.Raycast(ray, out hit, distToCamera, layerMask);
        if (isBlocked)
        {
            Vector3 globalHitPoint = hit.point;
            // Determine where to place the camera
            Vector3 cameraPlacement = Vector3.Lerp(globalHitPoint, startPos, 0.1f);
            float minDist = 0.3f;
            if (Vector3.Distance(cameraPlacement, startPos) < minDist)
            {
                // We are too close to the character, so turn them invisible
                mesh.enabled = false;
                cameraPlacement = globalHitPoint;
                if (Vector3.Distance(cameraPlacement, startPos) < minDist)
                {
                    cameraPlacement = startPos + (dirToCamera.normalized * minDist);
                }
            }
            // Convert to local coordinates and place the camera
            Vector3 localHitPoint = transform.InverseTransformPoint(cameraPlacement);
            //float hitDist = (cameraPlacement - startPos).magnitude;
            //AnimationHelpers.DrawRay(ray, Mathf.Min(distToCamera, hitDist), isBlocked, hit, Color.red, Color.green);
            currentCamera.transform.localPosition = localHitPoint;
        }
        else
            currentCamera.transform.localPosition = originalCameraPos;

        // Rotate camera with mouse
        currentCamera.transform.localRotation = Quaternion.Euler(new Vector3 (inputMouseY, inputMouseX, 0.0f) + startCameraRotation.eulerAngles);

    }

    private void OnAnimatorMove()
    {

        bool isGrounded;
        bool isNearGround;
        float sinceJump = Time.time - startJump;

        // How to get animator state
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);

        // Calculated if we should be grounded (which causes the animation to put feet on the ground)
        // Never allow yourself to be grounded if too close to when we jumped or in a jump prep state
        isNearGround = AnimationHelpers.CheckGroundNear(heroCollider.transform.position, jumpableGroundNormalMaxAngle, 1.0f, 1.0f, out hitLocation, true);
        if (animState.IsName("Jump Prep") || animState.IsName("Running Jump") || anim.IsInTransition(0) || sinceJump < 0.25f)
        {
            isGrounded = false;
            isNearGround = false;
        }
        else
        {
            isGrounded = AnimationHelpers.CheckGroundNear(transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out hitLocation);
        }

        // Signal to animator if we're near the ground - used to avoid falling animation if too near to the ground.
        anim.SetBool("nearGround", isNearGround);

        // Do jump
        if (inputJump && isGrounded)
        {
            inputJump = false;
            anim.SetBool("jump", true);
            anim.SetBool("grounded", false);
            anim.SetBool("doPickup", false);
            startJump = Time.time;
            sinceJump = 0f;
        }
        else
        {
            anim.SetBool("jump", false);
        }

        // Do actual movement
        anim.SetBool("grounded", isGrounded);
        // rbody.MoveRation only works inside of OnAnimatorMove()
        rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(inputTurn * Time.deltaTime * turnMaxSpeed, Vector3.up));
        //transform.rotation = rbody.rotation * Quaternion.AngleAxis(inputTurn * Time.deltaTime * turnMaxSpeed, Vector3.up);
        rbody.MovePosition(anim.rootPosition); // newRootPosition;
        //transform.position = anim.rootPosition;
        //// Decided to not use rotational root motion as is and instead handle without root motion
        //newRootRotation = anim.rootRotation;
        //this.transform.rotation = newRootRotation;

        // I needed this to force us to a grounded state if we get stuck in a jump due to the animation being unable to move to the final part of the jump animation
        if (!anim.GetBool("grounded") && sinceJump > 0.75f && !inputJump &&
            (rbody.velocity.x < 0.001 && rbody.velocity.x > -0.001) &&
            (rbody.velocity.y < 0.001 && rbody.velocity.y > -0.001) &&
            (rbody.velocity.z < 0.001 && rbody.velocity.z > -0.001))
        {
            anim.SetBool("grounded", true);
            heroCollider.center = origCenterCollider;
            heroCollider.height = origSizeCollider;
            if (hitLocation.y - heroCollider.transform.position.y > 0f)
                heroCollider.transform.position = hitLocation;
        }

        if (isNearGround && heroCollider.height == shrunkHeight)
        {
            heroCollider.center = origCenterCollider;
            heroCollider.height = origSizeCollider;
        }

        TakeActions();
    }


    private void TakeActions()
    {
        // How to get animator state
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);

        // Take action (drop stuff or pick it up)
        if (inputAction)
        {
            inputAction = false;
            HoldItem itemHolder = GetComponent<HoldItem>();
            bool isPickingup = animState.IsName("Pickup Object") || anim.GetBool("doPickup");
            bool isPlacing = animState.IsName("Place Tablet") || anim.GetBool("doPlacement") || animState.IsName("Grab Object");

            // Abort if doing a pick up or placement animation
            if (!isPickingup && !isPlacing)
            {
                // If near by item is an Interact then do animation
                if (itemHolder.nearItem != null && itemHolder.nearItem.tag == "Interact")
                {
                    anim.SetBool("doPickup", true);
                    anim.SetFloat("itemHeight", 1f);
                    interactionObject = itemHolder.nearItem.gameObject;
                }
                // If holding an item and near a tablet holder, then place it.
                else if (itemHolder.nearHolder != null && itemHolder.heldItem != null)
                {
                    anim.SetBool("doPlacement", true);
                    interactionObject = itemHolder.nearHolder.gameObject;
                }
                // If holding an item, throw it away
                else if (itemHolder.heldItem != null)
                {
                    DropItem(itemHolder);

                }
                // If not holding an item, but one is nearby, then pick it up
                else if (itemHolder.heldItem == null && itemHolder.nearItem != null)
                {
                    anim.SetBool("doPickup", true);
                    float itemHeight = itemHolder.nearItem.transform.position.y;
                    float playerHeight = transform.position.y;
                    anim.SetFloat("itemHeight", itemHeight - playerHeight);
                }
            }
        }
        else
        {
            anim.SetBool("doPickup", false);
            anim.SetBool("doPlacement", false);
        }

    }

    public void DropItem(HoldItem itemHolder)
    {
        // Make it so you can pick up tablets again
        Rigidbody item = itemHolder.heldItem.GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(8, 9, false);
        item.isKinematic = false;
        item.transform.parent = null;
        item.velocity = Vector2.zero;
        item.angularVelocity = Vector2.zero;
        item.AddForce(3.0f * this.transform.forward, ForceMode.VelocityChange);
        itemHolder.heldItem = null;
        anim.SetBool("doPickup", false);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);

            if (astate.IsName("Pickup Object") || astate.IsName("Grab Object") || astate.IsName("Place Tablet"))
            {
                if (doingIKAnim == false)
                {
                    doingIKAnim = true;
                    if (astate.IsName("Pickup Object") || astate.IsName("Grab Object"))
                    {
                        HoldItem itemHolder = GetComponent<HoldItem>();
                        saveTargetPos = itemHolder.nearItem.transform.position;
                    }
                    else if (astate.IsName("Place Tablet"))
                    {
                        saveTargetPos = interactionObject.transform.position;
                    }
                    else
                    {
                        // Default to your own position -- should never reach here
                        Debug.Log("Shouldn't be able to get to this line.");
                        saveTargetPos = transform.position;
                    }

                }
                float reachWeight = anim.GetFloat("reachObject");
                anim.SetLookAtWeight(reachWeight);
                anim.SetLookAtPosition(saveTargetPos);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, reachWeight);
                anim.SetIKPosition(AvatarIKGoal.RightHand, saveTargetPos);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
                doingIKAnim = false;
                saveTargetPos = Vector3.zero;
            }
        }
    }


    private void GrabObject()
    {
        anim.SetBool("doPickup", false);
        anim.SetFloat("itemHeight", 0f);
        HoldItem itemHolder = GetComponent<HoldItem>();
        if (itemHolder.nearItem != null)
            if (itemHolder.nearItem.tag == "Interact")
            {
                pullLever lever1 = itemHolder.nearItem.GetComponent<pullLever>();
                if (lever1 != null)
                    lever1.PullLever();
                FloorLever lever2 = itemHolder.nearItem.GetComponentInParent<FloorLever>();
                if (lever2 != null)
                    lever2.ActivateObject();
                FloorLeverPuzzle lever3 = itemHolder.nearItem.GetComponentInParent<FloorLeverPuzzle>();
                if (lever3 != null)
                    lever3.ActivateObject();
            }
            else
                itemHolder.ReceiveItem(itemHolder.nearItem);
    }


    private void OnJump(int type)
    {
        float sinceJump = Time.time - startJump;
        Vector3 jumpVector;
        float vely = anim.GetFloat("vely");
        if (vely >= -0.001f)
        {
            anim.SetBool("grounded", false);
            if (vely >= 0.9f) // Running Jump
            {
                jumpVector = new Vector3(0.0f, 30000.0f * jumpHeightMod, 0.0f);
                jumpVector += transform.forward * 25000f * vely * jumpForwardMod;
            }
            else
            {
                jumpVector = new Vector3(0.0f, 25000.0f * jumpHeightMod, 0.0f);
                jumpVector += transform.forward * 15000f * vely * jumpForwardMod;
            }
            rbody.AddForce(jumpVector);
            startJump = Time.time;
            // On jump, shrink collider temporarily
            heroCollider.height = shrunkHeight;
            heroCollider.center = new Vector3(0f, 14f, 0f);
        }
    }

    private void StartLanding()
    {
        heroCollider.center = origCenterCollider;
        heroCollider.height = origSizeCollider;
        if (hitLocation.y - heroCollider.transform.position.y > 0f)
            heroCollider.transform.position = hitLocation;
    }


    private void TouchPoint()
    {
        if (anim)
        {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);
            if (astate.IsName("Place Tablet"))
                PlaceTablet();
            else if (astate.IsName("Grab Object"))
                GrabObject();
        }
    }


    private void PlaceTablet()
    {
        if (interactionObject != null)
        {
            HoldItem heldItem = GetComponent<HoldItem>();
            GameObject tablet = heldItem.heldItem;
            GameObject slot = interactionObject.transform.Find("Slot").gameObject;
            if (tablet != null && slot != null && slot.transform.childCount == 0)
            {
                tablet.transform.SetParent(slot.transform);
                tablet.transform.localPosition = Vector3.zero;
                tablet.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                // If this is the correct tablet, then open the door, otherwise allow the tablet to still be picked up
                if (heldItem.heldItem.name == heldItem.nearHolder.correctTag)
                {
                    tablet.tag = "Untagged";
                    interactionObject.GetComponent<SphereCollider>().enabled = false;
                    OpenDoor doorScript = interactionObject.GetComponentInParent<OpenDoor>();
                    doorScript.Open();
                }
                Physics.IgnoreLayerCollision(8, 9, false);
                heldItem.heldItem = null;
                TextMeshProUGUI uiTextBox = GameObject.Find("Inventory Text").GetComponent<TextMeshProUGUI>();
                uiTextBox.text = "";
                heldItem.nearHolder = null;
                interactionObject = null;
                anim.SetBool("doPlacement", false);
            }
        }
    }
}
