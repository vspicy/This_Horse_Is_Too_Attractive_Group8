using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /*
     * Authors: [Ruffner, Kaylie], [Phurchpean, Ian]
     * Creation Date: [9-27-2025]
     * Summary: Handles the player's movement including walking, sprinting, jumping, slopes, and magnet effects.
     */

    [Header("Speed Settings")]
    private float speed; // Current movement speed
    public float walkSpeed; // Walking speed
    public float sprintSpeed; // Sprinting speed
    public KeyCode sprintKey = KeyCode.LeftShift; // Sprint key

    private Rigidbody rigidBody; // Player's Rigidbody
    public float maxSpeed; // Maximum speed limit
    public float sprintMultiplier = 2f; // Multiplier when sprinting

    [Header("Ground & Slope Detection")]
    public float playerHeight; // Height of the player for ground checks
    public LayerMask isGrounded; // Layers considered ground
    public bool onGround; // True if player is on ground
    private bool enableMovement; // Used for magnet movement

    [Header("Jumping")]
    bool readyToJump = true; // Ready to jump
    public KeyCode jumpKey = KeyCode.Space; // Jump key
    public float jumpForce; // Impulse applied when jumping
    public float jumpCooldown; // Time between jumps
    public float airMultiplier; // Movement multiplier while in air

    [Header("Slope Handling")]
    public float maxSlope; // Maximum angle considered a slope
    private RaycastHit onSlope; // Raycast hit info for slope detection
    private bool exitSlope; // Used to prevent slope forces after jumping

    [Header("Orientation")]
    public Transform orientation; // Reference for movement direction

    // Input and movement calculations
    private float horzInput;
    private float vertInput;
    private Vector3 moveDirection;

    [Header("Magnet Interaction")]
    public bool activeMagnet; // True if player is being attracted to an object

    public MovementStates state; // Current movement state
    public enum MovementStates { walking, sprinting, inAir }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true; // Prevent player from tipping over
    }

    void Update()
    {
        InputAxes();

        // Ground check (ignore triggers so player doesn't stand on trigger objects)
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGrounded, QueryTriggerInteraction.Ignore);

        Control(); // Limit speed
        StateHandler(); // Update movement state

        // Adjust drag: some friction on ground, none in air
        rigidBody.drag = onGround && !activeMagnet ? 2f : 0f;
    }

    private void FixedUpdate()
    {
        PlayerMove(); // Apply movement forces
    }

    /// <summary>
    /// Handles player input
    /// </summary>
    private void InputAxes()
    {
        horzInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        // Jumping
        if (Input.GetKey(jumpKey) && readyToJump && onGround)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    /// <summary>
    /// Apply movement forces to the player
    /// </summary>
    private void PlayerMove()
    {
        if (activeMagnet) return; // Don't move normally when magnetized

        moveDirection = orientation.forward * vertInput + orientation.right * horzInput;

        if (OnSlope() && !exitSlope)
        {
            // Only apply slope forces for non-trigger surfaces
            if (!onSlope.collider.isTrigger)
                rigidBody.AddForce(GetSlopeMoveDir() * speed * 10f, ForceMode.Force);

            rigidBody.useGravity = true;
        }
        else if (onGround)
        {
            rigidBody.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
            rigidBody.useGravity = true;
        }
        else
        {
            rigidBody.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
            rigidBody.useGravity = true;
        }
    }

    /// <summary>
    /// Determine current movement state and speed
    /// </summary>
    private void StateHandler()
    {
        if (onGround && Input.GetKey(sprintKey))
        {
            state = MovementStates.sprinting;
            speed = sprintSpeed;
        }
        else if (onGround)
        {
            state = MovementStates.walking;
            speed = walkSpeed;
        }
        else
        {
            state = MovementStates.inAir;
        }
    }

    /// <summary>
    /// Player jump logic
    /// </summary>
    private void Jump()
    {
        exitSlope = true;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z); // Reset vertical velocity
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitSlope = false;
    }

    /// <summary>
    /// Returns true if player is on a slope (ignores triggers)
    /// </summary>
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out onSlope, playerHeight * 0.5f + 0.3f, isGrounded, QueryTriggerInteraction.Ignore))
        {
            float angle = Vector3.Angle(Vector3.up, onSlope.normal);
            return angle < maxSlope && angle != 0;
        }
        return false;
    }

    /// <summary>
    /// Project movement direction onto slope plane
    /// </summary>
    private Vector3 GetSlopeMoveDir()
    {
        return Vector3.ProjectOnPlane(moveDirection, onSlope.normal).normalized;
    }

    /// <summary>
    /// Limit player speed on ground, slopes, and in air
    /// </summary>
    private void Control()
    {
        if (OnSlope() && !exitSlope)
        {
            if (rigidBody.velocity.magnitude > speed)
                rigidBody.velocity = rigidBody.velocity.normalized * speed;
        }
        else
        {
            Vector3 vFlat = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            if (vFlat.magnitude > speed)
            {
                Vector3 vLimit = vFlat.normalized * speed;
                rigidBody.velocity = new Vector3(vLimit.x, rigidBody.velocity.y, vLimit.z);
            }
        }
    }

    /// <summary>
    /// Jump towards a position (magnet interaction)
    /// </summary>
    public void JumpToPosition(Vector3 positionT, float trajectory)
    {
        activeMagnet = true;
        rigidBody.velocity = CalculateJumpVelocity(transform.position, positionT, trajectory);
        Invoke(nameof(SetVelocity), 4f);
        Invoke(nameof(ResetRestrictions), 2f);
    }

    private Vector3 setVelocity;

    private void SetVelocity()
    {
        enableMovement = true;
        rigidBody.velocity = setVelocity;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    private void ResetRestrictions()
    {
        activeMagnet = false;
    }

    /// <summary>
    /// Reset magnet movement on collision with non-attract objects
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovement && !collision.gameObject.GetComponent<AttractFunction>())
        {
            enableMovement = false;
            ResetRestrictions();
        }
    }
}
