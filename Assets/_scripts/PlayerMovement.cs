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
    public float speed;

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
    bool readyToJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    // Get player orientation
    public Transform orientation;

    // Get the horizontal and vertical positions of the player, and the move direction
    private float horzInput;
    private float vertInput;
    private Vector3 moveDirection;
  
    // Check if the magnet is currently active
    public bool activeMagnet;

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

        // apply max speed
        if (onGround && !activeMagnet)
            rigidBody.drag = maxSpeed; // Set maximum Speed of the object
        else if (!onGround)
            rigidBody.drag = 0; // There is less friction and resistance in the air
    }

    private void FixedUpdate()
    {
        PlayerMove();
        Control();
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

        // On the ground
        if (onGround)
            rigidBody.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
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
    /// The player jumps
    /// </summary>
    private void Jump()
    {
        // reset y velocity
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    /// <summary>
    /// Provide a hard limit on the player's movement.
    /// </summary>
    private void Control()
    {
        Vector3 vFlat = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        //limit when needed
        if (vFlat.magnitude > speed)
        {
            Vector3 vLimit = vFlat.normalized * speed;
            rigidBody.velocity = new Vector3(vLimit.x, rigidBody.velocity.y, vLimit.z);
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

