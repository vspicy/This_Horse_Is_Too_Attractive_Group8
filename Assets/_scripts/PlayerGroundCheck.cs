using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [10-30-2025]
 * Summary: Detects when the player lands on the ground or enters a magnet and re-enables the Movement script if it was disabled.
 *         
 */

public class PlayerGroundCheck : MonoBehaviour
{
    public Movement movementScript;          // Reference to player's Movement script

    public float playerHeight = 2f;          // Approximate height of the player
    public LayerMask groundLayer;            // Layers considered ground
    public float groundCheckOffset = 0.2f;   // Extra buffer distance

    private bool isGrounded;
    private bool wasGrounded;

    void Start()
    {
        // Automatically find the Movement script if not assigned
        if (movementScript == null)
            movementScript = GetComponent<Movement>();
    }

    void Update()
    {
        // Perform ground check using a downward raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + groundCheckOffset, groundLayer, QueryTriggerInteraction.Ignore);

        // Detect landing
        if (isGrounded)
        {
            //Debug.Log("Player has landed on the ground.");
            ReenableMovement();
        }

        wasGrounded = isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered a magnet
        if (other.GetComponent<TestAttraction>() != null)
        {
            Debug.Log("Player entered a magnet trigger.");
            ReenableMovement();
        }
    }

    // Turn on movement
    private void ReenableMovement()
    {
        if (movementScript != null && !movementScript.enabled)
        {
            movementScript.enabled = true;
            Debug.Log("Movement script re-enabled.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the ground check ray in the Scene view
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (playerHeight * 0.5f + groundCheckOffset));
    }
}
