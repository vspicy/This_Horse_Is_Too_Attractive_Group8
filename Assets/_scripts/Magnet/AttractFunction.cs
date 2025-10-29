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
    public Rigidbody playerRB;
    public GameObject magnetSurface;

    [Header("Variables")]
    public bool magnetOn;
    public float velocity;
    private bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        
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
            playerRB = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If LMB pressed:
        {
            Debug.Log("LMB Down: Magnet Active");
            magnetOn = true;

            Attract();
        }

        else if (Input.GetMouseButtonUp(0)) // If LMB released:
        {
            Debug.Log("LMB Up: Magnet Inactive");

            magnetOn = false;
        }
    }

    /// <summary>
    /// Run this code whenever the player is in range of the magnet
    /// </summary>
    private void Attract()
    {
        if (magnetOn == true && inRange == true)
            transform.position = Vector3.MoveTowards(this.transform.position, magnetSurface.transform.position, velocity * Time.deltaTime);
    }
}
