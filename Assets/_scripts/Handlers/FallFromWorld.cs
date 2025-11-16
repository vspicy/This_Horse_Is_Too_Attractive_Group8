using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Name: Ian Phurchpean & Hayden Bose
 * Creation Date: 15 October 2025
 * Summary: When the player falls past a certain point, respawn them back at the designated respawn point and add to death count.
 */

public class FallFromWorld : MonoBehaviour
{
    public TMP_Text deathCountDisplay;
    public int deathCount;

    // Get y coord threshold (designated kill level)
    public float killFloor;


    void Start()
    {
        deathCount = 0;
        deathCountDisplay.text = "Death Count: " + deathCount.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))  // Reset death count everytime the player restarts manually
        {
            deathCount = 0;
            deathCountDisplay.text = "Death Count: " + deathCount.ToString();
        }
    }

    // Call this update at fixed time intervals
    private void FixedUpdate()
    {
        if (transform.position.y < killFloor)
        {
            deathCount++;
            deathCountDisplay.text = "Death Count: " + deathCount.ToString();
            transform.position = new Vector3(0f, 4f, -15f);
        }
    } 
}
