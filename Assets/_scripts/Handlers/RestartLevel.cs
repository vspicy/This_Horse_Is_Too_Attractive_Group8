using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    // Initialize Variables
    private Rigidbody rigidBody; // Get Object Rigidbody Component

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); // Get player rigidbody to reset velocity on restart
    }

    // Update is called once per frame
    void Update()
    {
        // Reset player position
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0f, 4f, -15f); // Reset player to start
            rigidBody.velocity = Vector3.zero; // Reset velocity
        }
    }
}
