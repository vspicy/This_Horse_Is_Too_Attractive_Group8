using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [12-4-2025]
 * Summary: This script handles the repulsion mechanic for the player, where the player can charge up and repel themselves off magnetic surfaces
 */
public class RepulsionTransform : MonoBehaviour
{
    private float moveSpeed = 2;
    public Transform playerTransform;
    public GameObject thirdPresonCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        print("Transform Active");
    }

    // When WASD/Space/Ctrl is pressed, slowly move the player based on direction of pressed key and the rotation of the camera
    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //playerTransform.localPosition += Vector3.left * moveSpeed * Time.deltaTime;
            playerTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime, thirdPresonCamera.transform);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //playerTransform.localPosition += Vector3.forward * moveSpeed * Time.deltaTime;
            playerTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, thirdPresonCamera.transform);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //playerTransform.localPosition += Vector3.back * moveSpeed * Time.deltaTime;
            playerTransform.Translate(Vector3.back * moveSpeed * Time.deltaTime, thirdPresonCamera.transform);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //playerTransform.position += Vector3.right * moveSpeed * Time.deltaTime;
            playerTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime, thirdPresonCamera.transform);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //playerTransform.position += Vector3.up * moveSpeed * Time.deltaTime;
            playerTransform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            //playerTransform.position += Vector3.down * moveSpeed * Time.deltaTime;
            playerTransform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
