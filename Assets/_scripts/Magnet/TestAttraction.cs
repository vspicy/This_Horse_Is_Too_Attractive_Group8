using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [10-29-2025]
 * Summarization: [This script handles the attraction mechanic for the player, where the player can attract themselves to magnetic surfaces]
 */

public class TestAttraction : MonoBehaviour
{
    private float attractionForce = 15f;
    private float maxForce = 75f;
    private float accelerationRate = 10f;
    private float currentForceMultiplier = 0f;
    private bool playerInRange;
    private Rigidbody playerRB;
    void Start()
    {
        playerInRange = false;
    }

    // On player exit, change InRange boolean to true
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRB = other.GetComponent<Rigidbody>();
            print("Player has entered range");
            playerInRange = true;
            currentForceMultiplier = 0f;
        }
    }

    // On player exit, change InRange boolean to false
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            print("Player has left range");
            playerRB = null;
        }
    }

    void Update()
    {
        //Run Attract if mouse 0 is held down
        if (Input.GetKey(KeyCode.Mouse0) && playerInRange == true)
        {
            print("Attract");
            Attract();
        }
        else
        {
            currentForceMultiplier = Mathf.MoveTowards(currentForceMultiplier, 0f, accelerationRate * Time.fixedDeltaTime); //slow down acceleration multiplier
        }
    }

    public void Attract()
    {
        Vector3 direction = (transform.position - playerRB.position).normalized;

        // Ramp up the pull force
        currentForceMultiplier = Mathf.MoveTowards(currentForceMultiplier, 1f, accelerationRate * Time.deltaTime);

        // Calculate force and clamp magnitude
        Vector3 force = direction * attractionForce * currentForceMultiplier;
        force = Vector3.ClampMagnitude(force, maxForce);

        // Apply force
        playerRB.AddForce(force, ForceMode.Force);

        if (playerRB.velocity.magnitude > 15f)
        {
            playerRB.velocity = playerRB.velocity.normalized * 15f;
        }

    }
}
