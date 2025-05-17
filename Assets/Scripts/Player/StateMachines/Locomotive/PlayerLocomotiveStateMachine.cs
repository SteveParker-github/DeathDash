using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerLocomotiveStateMachine : MonoBehaviour
{
    private CharacterController controller;
    private PlayerControls playerControls;
    public float WALKSPEED = 4.0f;
    private bool isDodging;
    private float dodgeCooldown = 0.0f;
    private float MAXDODGECOOLDOWNTIMER = .5f;
    private float dodgeDir;
    private PlayerLocomotiveBaseState currentState;
    private PlayerLocomotiveStateFactory factory;
    private Vector3 currentMovement;
    private Vector3 appliedMovement;
    private Quaternion targetRot;

    // gravity variables
    private float gravity = -9.8f;
    public float fallSpeed = -20f;

    //jumping variables
    private float initalJumpVelocity;
    private float maxJumpHeight = 5.0f;
    private float maxJumpTime = 0.75f;
    private bool isJumping = false;
    public LayerMask groundMask;
    public Transform groundPoint;

    //rotation variables
    private float rotationSpeed = 30;

    public PlayerLocomotiveBaseState CurrentState { get => currentState; set => currentState = value; }
    public CharacterController Controller { get => controller; }
    public PlayerControls PlayerControls { get => playerControls; }
    public float DodgeDir { get => dodgeDir; set => dodgeDir = value; }
    public bool IsDodging { set => isDodging = value; }
    public bool IsJumping { set => isJumping = value; }
    public float CurrentMovementY { get => currentMovement.y; set => currentMovement.y = value; }
    public float CurrentMovementZ { get => currentMovement.z; set => currentMovement.z = value; }
    public float AppliedMovementY { get => appliedMovement.y; set => appliedMovement.y = value; }
    public float AppliedMovementZ { get => appliedMovement.z; set => appliedMovement.z = value; }
    public float InitalJumpVelocity { get => initalJumpVelocity; }
    public float Gravity { get => gravity; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        targetRot = transform.rotation;
        SetupJumpVariables();
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initalJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponent<PlayerControls>();
        factory = new PlayerLocomotiveStateFactory(this);
        currentState = factory.Grounded();
        currentState.EnterState();
    }

    private void HandleRotation()
    {
        playerControls.IsRotating = transform.rotation != targetRot;
        Vector3 lookAt = Vector3.zero;

        if (isDodging)
        {
            lookAt.z = dodgeDir;
        }
        else
        {
            lookAt.z = float.IsNegative(playerControls.MoveInput.x) ? -1 : 1;
        }

        Quaternion currentRot = transform.rotation;

        if (playerControls.MoveInput != Vector2.zero || currentRot != targetRot || isDodging)
        {
            if (playerControls.MoveInput != Vector2.zero || isDodging)
            {
                targetRot = Quaternion.LookRotation(lookAt);
            }

            // transform.rotation = Quaternion.Slerp(currentRot, targetRot, rotationSpeed * Time.deltaTime);
            float rotDistance = Vector3.Distance(targetRot.eulerAngles, currentRot.eulerAngles);

            if (rotDistance <= 10f)
            {
                transform.rotation = targetRot;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(currentRot, targetRot.normalized, rotationSpeed * Time.deltaTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateStates();

        if (dodgeCooldown > 0)
        {
            dodgeCooldown -= Time.deltaTime;
        }

        controller.Move(appliedMovement * Time.deltaTime);
        HandleRotation();
    }

    public void SetDodgeCooldown()
    {
        dodgeCooldown = MAXDODGECOOLDOWNTIMER;
        isDodging = false;
    }

    // public bool IsGrounded()
    // {
    //     Vector3 test = transform.position;
    //     test.y += 0.25f;
    //     bool isGrounded = Physics.BoxCast(test, new Vector3(0.5f, 0.01f, 0.15f), -transform.up, transform.rotation, 0.3f);
    //     Debug.DrawRay(test, -transform.up * 0.3f);
    //     Debug.Log("physics check: " + isGrounded + ". controller check: " + Controller.isGrounded);
    //     return isGrounded;
    // }

        public bool IsGrounded()
    {
        float groundedOffset = -0.1f;
        float groundedRadius = 0.2f;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
		bool isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundMask, QueryTriggerInteraction.Ignore);
        Debug.DrawRay(spherePosition, -transform.up * 0.3f);
        //Debug.Log("physics check: " + isGrounded + ". controller check: " + Controller.isGrounded);
        return isGrounded;
    }

    			
}
