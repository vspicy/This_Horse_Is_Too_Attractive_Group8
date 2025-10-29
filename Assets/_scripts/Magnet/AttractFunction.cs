using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 28 October 2025
 * Objective: When a magnetic surface is close to the player, attract them to it.
 */

public class AttractFunction : MonoBehaviour
{
    [Header("References")]
    private Rigidbody playerRB;

    [Header("Variables")]
    public float velocity;
    private bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    /// <summary>
    /// When the player enters the radius of a magnet
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRB = other.GetComponent<Rigidbody>();
            print("Player has entered range");
            inRange = true;
        }
    }

    /// <summary>
    /// When the player exits the radius of a magnet
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            print("Player has left range");
            playerRB = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If LMB pressed:
        {
            Debug.Log("LMB Down: Magnet Active");
            if (inRange == true)
            {
                Attract();
            }
        }

        else if (Input.GetMouseButtonUp(0)) // If LMB released:
        {
            Debug.Log("LMB Up: Magnet Inactive");
        }
    }

    /// <summary>
    /// Run this code whenever the player is in range of the magnet
    /// </summary>
    private void Attract()
    {
        // Calculate direction to the source
        Vector3 dir = (transform.position - playerRB.position).normalized;

        playerRB.AddForce(dir * velocity, ForceMode.Impulse);
    }
}
