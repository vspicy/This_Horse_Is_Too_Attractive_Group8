using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [10-28-2025]
 * Summarization: [This script handles the repulsion mechanic for the player, where the player can repel themselves off magnetic surfaces]
 */
public class Repulsion : MonoBehaviour
{
    public float repulsionForce = 50f;
    public float cooldownTime = 1f;
    private float lastRepulseTime;
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
        if (Input.GetKey(KeyCode.Mouse1) && playerInRange == true && Time.time >= lastRepulseTime + cooldownTime)
        {
            print("Repel");
            Repulse();
            lastRepulseTime = Time.time;
        }
    }

    public void Repulse()
    {
        // calculates direction away from the source
        Vector3 direction = (playerRB.position - transform.position).normalized; 

        //clears velocity
        playerRB.velocity = new Vector3(0, 0, 0);

        //applies the force
        playerRB.AddForce(direction * repulsionForce, ForceMode.Impulse);
    }
}
