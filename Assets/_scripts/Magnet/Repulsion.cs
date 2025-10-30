using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [10-29-2025]
 * Summarization: [This script handles the repulsion mechanic for the player, where the player can repel themselves off magnetic surfaces]
 */
public class Repulsion : MonoBehaviour
{
    private float repulsionForce = 20f;
    private float cooldownTime = 2f;
    private float lastRepulseTime;
    private bool playerInRange;
    private Rigidbody playerRB;

    // Reference to the player's Movement script
    private Movement playerMovement;

    void Start()
    {
        playerInRange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // On player exit, change InRange boolean to true
        if (other.CompareTag("Player"))
        {
            playerRB = other.GetComponent<Rigidbody>();
            playerMovement = other.GetComponent<Movement>(); // Get the Movement script
            print("Player has entered range");
            playerInRange = true;
        }
    }

    // On player exit, change InRange boolean to false
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerRB = null;
            playerMovement = null;
            print("Player has left range");
        }
    }

    void Update()
    {
        if (playerRB != null)
        {
            // Cap player speed
            if (playerRB.velocity.magnitude > 40)
            {
                playerRB.velocity = playerRB.velocity.normalized * 40;
            }
        }

        // Right click to activate repulsion
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerInRange && Time.time >= lastRepulseTime + cooldownTime)
        {
            print("Repel");
            Repulse();
            lastRepulseTime = Time.time;
        }
    }

    public void Repulse()
    {
        if (playerRB == null) return;

        // Disable the Movement script so it doesn’t clamp velocity
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            print("Movement script disabled for repulsion");
        }

        // Calculate direction away from the surface
        Vector3 direction = (playerRB.position - transform.position).normalized;

        // Clear current velocity
        playerRB.velocity = Vector3.zero;

        // Apply instantaneous repulsion force
        playerRB.AddForce(direction * repulsionForce, ForceMode.VelocityChange);
    }
}
