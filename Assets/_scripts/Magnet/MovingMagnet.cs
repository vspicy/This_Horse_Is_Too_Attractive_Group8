using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 4 December 2025
 * Objective: Summary: The magnets can move it move it now.
 */
public class MovingMagnet : MonoBehaviour
{
    [Header("References")]
    // Initialize variables
    public GameObject movingMagnet;
    public GameObject initialPosition;
    public GameObject finalPosition;

    public float velocity;
    private Vector3 startPos, endPos;
    [SerializeField] private bool isMovingForward = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = initialPosition.transform.position;
        endPos = finalPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingForward)
            movingMagnet.transform.position = Vector3.MoveTowards(movingMagnet.transform.position, finalPosition.transform.position, velocity * Time.deltaTime); // Move magnet towards the final position
        
        if (movingMagnet.transform.position == endPos) // Reaches endPos
        {
            isMovingForward = false;
        }

        if (!isMovingForward)
            movingMagnet.transform.position = Vector3.MoveTowards(movingMagnet.transform.position, initialPosition.transform.position, velocity * Time.deltaTime); // Return to the original position

        if (movingMagnet.transform.position == startPos) // Reaches startPos
        {
            isMovingForward = true;
        }
    }
}
