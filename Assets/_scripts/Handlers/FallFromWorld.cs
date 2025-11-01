using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 15 October 2025
 * Objective: Maybe the player falls out of the world. Make sure that they respawn too.
 */

public class FallFromWorld : MonoBehaviour
{
    // Get y coord threshold (designated kill level)
    public float killFloor;

    // Call this update at fixed time intervals
    private void FixedUpdate()
    {
        if (transform.position.y < killFloor)
        {
            transform.position = new Vector3(0f, 4f, -15f);
        }
    } 
}
