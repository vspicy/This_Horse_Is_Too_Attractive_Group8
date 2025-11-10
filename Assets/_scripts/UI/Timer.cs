using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
 * Name: Ian Phurchpean
 * Last Updated: 9 November 2025
 * In-game UI Manager to represent the player's time.
*/

public class Timer : MonoBehaviour
{
    // Initialize Variables
    public TMP_Text time; // time
    float givenTime;

    // Update is called once per frame
    void Update()
    {
        Stopwatch();
    }

    /// <summary>
    /// Store this stopwatch on the UI to always have the player's time present.
    /// </summary>
    public void Stopwatch()
    {
        // Keep track of the time
        givenTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(givenTime / 60); // find minute (Convert float to int)
        int seconds = Mathf.FloorToInt(givenTime % 60); // find second (Convert float to int)

        time.text = "TIME: " + string.Format("{0:00}:{1:00}", minutes, seconds); // display current time
    }
}
