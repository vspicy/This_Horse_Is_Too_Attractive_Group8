using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [10-29-2025]
 * Summary: [This script handles the attraction mechanic for the player, where the player can attract themselves to magnetic surfaces]
 */

public class TestAttraction : MonoBehaviour
{
    private float attractionForce = 20f;
    private float maxForce = 60f;
    private float accelerationRate = 14f;
    private float currentForceMultiplier = 0f;
    private bool playerInRange;
    private Rigidbody playerRB;

    [Header("Color Manager")]
    [SerializeField]
    private Renderer magneticObject;
    private Movement playerMovement;
    private Color magnetColor = new Color(0.20f, 0.20f, 0.20f);

    void Start()
    {
        playerInRange = false;
        magneticObject.material.color = magnetColor;
    }

    // On player exit, change InRange boolean to true
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRB = other.GetComponent<Rigidbody>();
            playerMovement = other.GetComponent<Movement>();
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
            playerMovement = null;
            print("Player has left range");
            playerRB = null;
            magneticObject.material.color = magnetColor;
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

        if (Input.GetMouseButtonUp(0))
        {
            magneticObject.material.color = magnetColor;
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

        if (playerMovement.enabled == false)
        {
            playerMovement.enabled = true;
        }

        magneticObject.material.color = Color.green;

    }
}
