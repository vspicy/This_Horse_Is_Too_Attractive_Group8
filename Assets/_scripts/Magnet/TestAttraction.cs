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
    private float attractionForce = 4f;
    private float maxForce = 10f;
    private float accelerationRate = 2f;
    private float currentForceMultiplier = 0f;
    private bool playerInRange;
    private Rigidbody playerRB;
    void Start()
    {
        playerInRange = false;
    }

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
        if (Input.GetKey(KeyCode.Mouse0) && playerInRange == true)
        {
            print("Attract");
            Attract();
        }
        else
        {
            currentForceMultiplier = Mathf.MoveTowards(currentForceMultiplier, 0f, accelerationRate * Time.fixedDeltaTime);
        }
    }

    public void Attract()
    {
        Vector3 direction = (transform.position - playerRB.position).normalized;

        // Smoothly ramp up the pull force
        currentForceMultiplier = Mathf.MoveTowards(currentForceMultiplier, 1f, accelerationRate * Time.fixedDeltaTime);

        // Compute force and clamp magnitude
        Vector3 force = direction * attractionForce * currentForceMultiplier;
        force = Vector3.ClampMagnitude(force, maxForce);

        // Apply force
        playerRB.AddForce(force, ForceMode.Force);

        if (playerRB.velocity.magnitude > 12f)
        {
            //playerRB.velocity = playerRB.velocity.normalized * 12f;
        }

    }
}
