using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [10-29-2025]
 * Summary: This script handles the repulsion mechanic for the player, where the player can charge up and repel themselves off magnetic surfaces
 */
public class Repulsion : MonoBehaviour
{
    private float repulsionForce = 25f;
    private float minForce = 5f;
    private float maxForce = 25f;
    private float cooldownTime = 2f;
    private float lastRepulseTime;
    private bool playerInRange;
    private float minDistance = 1.5f;
    private float maxDistance = 10f;
    private float currentCharge = 0f;
    private float chargeTime = 1f;
    private bool fullyCharged = false;
    private float distanceFromPlayer;
    private float slowForce = 15;
    private Rigidbody playerRB;

    // Reference to the player's Movement script
    private Movement playerMovement;

    [Header("Color Manager")]
    [SerializeField]
    private Renderer magneticObject; // Reference to Object X
    private Color magnetColor = new Color(0.20f, 0.20f, 0.20f); // Reference to color

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
            magneticObject.material.color = Color.gray;
        }
    }

    // On player exit, change InRange boolean to false
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRB.useGravity = true;
            playerInRange = false;
            playerRB = null;
            playerMovement = null;
            print("Player has left range");
            magneticObject.material.color = magnetColor;
        }
    }

    void Update()
    {
        //increases and decreases repulsion force based on distance from magnet
        if (playerInRange && playerRB != null)
        {
            //calculates and assignes distance from object to distanceFromPlayer variable
            distanceFromPlayer = Vector3.Distance(transform.position, playerRB.position);

            //calculates distance into a 0-1 range used for lerping
            float rfClamp = Mathf.InverseLerp(maxDistance, minDistance, distanceFromPlayer);

            //lerps the repuslion force between minForce and maxForce based on the 0-1 range from above
            repulsionForce = Mathf.Lerp(minForce, maxForce, rfClamp);
        }
        if (playerRB != null)
        {
            // Cap player speed
            if (playerRB.velocity.magnitude > 40)
            {
                playerRB.velocity = playerRB.velocity.normalized * 40;
            }
        }

        // Start charging while holding right click and if no cooldown
        if (Input.GetKey(KeyCode.Mouse1) && playerInRange && Time.time >= lastRepulseTime + cooldownTime)
        {

            playerRB.useGravity = false;
            playerMovement.enabled = false;


            // Slows player towards zero velocity
            playerRB.velocity = Vector3.MoveTowards(playerRB.velocity, Vector3.zero, Time.deltaTime * slowForce);


            currentCharge += Time.deltaTime / chargeTime;
            currentCharge = Mathf.Clamp01(currentCharge);

            // Checks if fully charged
            if (currentCharge >= 1f && !fullyCharged)
            {
                fullyCharged = true;
                magneticObject.material.color = Color.red;
                Debug.Log("Repulsion fully charged!");
            }
        }

        // Repulse on button release and if fully charged 
        if (Input.GetKeyUp(KeyCode.Mouse1) && playerInRange)
        {
            playerRB.useGravity = true;
            playerMovement.enabled = true;

            if (fullyCharged && playerInRange)
            {
                Repulse();
                lastRepulseTime = Time.time;
            }

            ResetCharge();
            
        }
    }

    public void Repulse()
    {

        playerMovement.enabled = false;

        // Calculates the direction away from the surface
        Vector3 direction = (playerRB.position - transform.position).normalized;

        // Clears current velocity
        playerRB.velocity = Vector3.zero;

        // Applys repulsion force
        playerRB.AddForce(direction * repulsionForce, ForceMode.VelocityChange);
    }

    // Resets repulsion charge progress 
    private void ResetCharge()
    {
        fullyCharged = false;
        currentCharge = 0f;
    }
}
