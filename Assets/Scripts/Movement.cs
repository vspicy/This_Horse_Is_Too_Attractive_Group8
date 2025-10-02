using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    /*
     * Authors: [Ruffner, Kaylie]
     * Creation Date: [9-27-2025]
     * Summary: [This script handles the player's movement (running, sprinting, jumping)]
     */

    public float speed;
    public float jumpForce;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    /// <summary>
    /// this handes the movement of the player, checking the input of each keybind
    /// </summary>
    private void PlayerMove()
    {
        // gets the key input (A) to go left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // gets the key input (D) to go right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // gets the key input (W) to go forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        // gets the key input (S) to go backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// this will handle the jump mechanic
    /// </summary>
    private void PlayerJump()
    {
        // gets the key input (space) to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;

            // if the raycast returns that it hits something that means the player is touching the ground.
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                Debug.Log("Touching the ground.");
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Player is not touching the ground.");
            }
        }
    }
}
