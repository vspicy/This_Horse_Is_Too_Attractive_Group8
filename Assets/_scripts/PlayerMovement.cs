using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    /*
     * Authors: [Ruffner, Kaylie]
     * Creation Date: [9-27-2025]
     * Summary: [This script handles the player's movement (running, sprinting, jumping)]
     */
    
    // Set player speed
    [Header("Speed")]
    private float speed;
    public float walkSpeed;
    public float sprintSpeed;
    public KeyCode sprintKey = KeyCode.LeftShift;

    // Get Rigidbody of the player
    private Rigidbody rigidBody;
    public float maxSpeed;
    public float sprintMultiplier = 2f;

    // Get height of the player
    public float playerHeight;
    public LayerMask isGrounded;
    public bool onGround;
    private bool enableMovement;

    // Jumping
    [Header("Jumping")]
    bool readyToJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [Header("Slope")]
    public float maxSlope;
    private RaycastHit onSlope;
    private bool exitSlope;

    // Get player orientation
    public Transform orientation;

    // Get the horizontal and vertical positions of the player, and the move direction
    private float horzInput;
    private float vertInput;
    private Vector3 moveDirection;
  
    // Check if the magnet is currently active
    public bool activeMagnet;

    public MovementStates state;

    public enum MovementStates
    {
        walking,
        sprinting,
        inAir
    }

    void Start()
    {
        // Get the rigidBody component
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true; // Freeze rotational movement of the Rigidbody (Does not fall over)
    }

    // Update is called once per frame
    void Update()
    {
        InputAxes();
        // Raycast to the ground to tell when the player is grounded
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGrounded);

        Control();
        StateHandler();

        // apply max speed
        if (onGround && !activeMagnet)
            rigidBody.drag = maxSpeed; // Set maximum Speed of the object
        else if (!onGround)
            rigidBody.drag = 0; // There is less friction and resistance in the air
    }

    private void FixedUpdate()
    {
        PlayerMove();
        
    }

    /// <summary>
    /// Get horizontal and vertical axes of the player in any current state.
    /// </summary>
    private void InputAxes()
    {
        horzInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && onGround)
        {
            readyToJump = false;

            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    /// <summary>
    /// this handes the movement of the player, checking the input of each keybind
    /// </summary>
    private void PlayerMove()
    {
        if (activeMagnet) return;

        // Calculate direction of movement
        moveDirection = orientation.forward * vertInput + orientation.right * horzInput; // Always walk in the direction the player

        // On a Slope
        if (OnSlope() && !exitSlope)
        {
            rigidBody.AddForce(GetSlopeMoveDir() * speed * 20f, ForceMode.Force);

            // While traveling upward, force the player towards the ground to prevent bouncing
            if (rigidBody.velocity.y > 0)
                rigidBody.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // On the ground
        if (onGround)
            rigidBody.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);

        // In the air
        else if (!onGround)
            rigidBody.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);

        // While not on a slope, turn on the gravity
        // While on a slope, turn off the gravity
        rigidBody.useGravity = !OnSlope();

        // gets the key input (A) to go left
        /*
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // gets the key input (D) to go right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // gets the key input (W) to go forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        // gets the key input (S) to go backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        */
    }

    /// <summary>
    /// Handles the current state of the player.
    /// </summary>
    private void StateHandler()
    {
        // If Sprinting
        if (onGround && Input.GetKey(sprintKey))
        {
            state = MovementStates.sprinting;
            speed = sprintSpeed;
        }
        // If Walking
        else if (onGround)
        {
            state = MovementStates.walking;
            speed = walkSpeed;
        }
        // In Air
        else
        {
            state = MovementStates.inAir;
        }
    }

    /// <summary>
    /// The player jumps
    /// </summary>
    private void Jump()
    {
        exitSlope = true;

        // reset y velocity
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitSlope = false;
    }

    /// <summary>
    /// Returns value if Player is on a slope
    /// </summary>
    /// <returns></returns>
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out onSlope, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, onSlope.normal);
            return angle < maxSlope && angle != 0; // Return the value of an angle if it is less than the max slope and not equal to 0
        }
        return false; // Return nothing if there is no slope
    }

    /// <summary>
    /// Project normal move direction on the slope
    /// </summary>
    /// <returns></returns>
    private Vector3 GetSlopeMoveDir()
    {
        return Vector3.ProjectOnPlane(moveDirection, onSlope.normal).normalized;
    }

    /// <summary>
    /// Provide a hard limit on the player's movement.
    /// </summary>
    private void Control()
    {

        // Limit the speed on a slope
        if (OnSlope() && !exitSlope)
        {
            if (rigidBody.velocity.magnitude > speed)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * speed;
            }
        }

        else // Limit the speed on the ground and the air
        {
            Vector3 vFlat = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

            // limit when needed
            if (vFlat.magnitude > speed)
            {
                Vector3 vLimit = vFlat.normalized * speed;
                rigidBody.velocity = new Vector3(vLimit.x, rigidBody.velocity.y, vLimit.z);
            }
        }  
    }

    public void JumpToPosition(Vector3 positionT, float trajectory)
    {
        activeMagnet = true;
        rigidBody.velocity = CalculateJumpVelocity(transform.position, positionT, trajectory);
        Invoke(nameof(SetVelocity), 4f);
        Invoke(nameof(ResetRestrictions), 2f); // Something went wrong
    }

    private Vector3 setVelocity;

    /// <summary>
    /// Set velocity of the player while attracted to other objects.
    /// </summary>
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
    
    /// <summary>
    /// Reset any and all functions that may have been disabled.
    /// </summary>
    private void ResetRestrictions()
    {
        activeMagnet = false;
    }

    /// <summary>
    /// Activate on collision with an object
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovement)
        {
            enableMovement = false;
            ResetRestrictions();

            GetComponent<MagnetPull>().StopMagnet();
        }
    }
}

