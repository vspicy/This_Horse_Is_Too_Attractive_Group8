using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [10-30-2025]
 * Summary: Detects when the player lands on the ground or enters a magnet trigger, and re-enables the Movement script if it was disabled.
 *         
 */

[RequireComponent(typeof(Rigidbody))]
public class PlayerGroundCheck : MonoBehaviour
{
    [Header("References")]
    public Movement movementScript;          // Reference to player's Movement script

    [Header("Ground Check Settings")]
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
        // Perform ground check using a downward ray
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + groundCheckOffset, groundLayer, QueryTriggerInteraction.Ignore);

        // Detect landing (was in air, now grounded)
        if (!wasGrounded && isGrounded)
        {
            Debug.Log("Player has landed on the ground.");
            ReenableMovement();
        }

        wasGrounded = isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered a magnet trigger
        if (other.GetComponent<TestAttraction>() != null)
        {
            Debug.Log("Player entered a magnet trigger.");
            ReenableMovement();
        }
    }

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
