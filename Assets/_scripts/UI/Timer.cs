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
    [Header("References")]
    public TMP_Text time; // time
    public bool playing;
    float givenTime;

    public float finalTime;

    [Header("Time")]
    int minutes;
    int seconds;
    int milliseconds;

    // Update is called once per frame
    void Update()
    {
        Stopwatch();
        finalTime = givenTime;


        if (Input.GetKeyDown(KeyCode.R))
        {
            minutes = 0;
            seconds = 0;
            milliseconds = 0;
        }

    }

    /// <summary>
    /// Store this stopwatch on the UI to always have the player's time present.
    /// </summary>
    public void Stopwatch()
    {
        if (playing == true)
        {
            // Keep track of the time
            givenTime += Time.deltaTime;
            minutes = Mathf.FloorToInt(givenTime / 60); // find minute (Convert float to int)
            seconds = Mathf.FloorToInt(givenTime % 60); // find second (Convert float to int)
            milliseconds = Mathf.FloorToInt((givenTime * 100) % 100); // find millisecond (Convert float to int)

            time.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + "." + milliseconds.ToString("00"); // Display time on the screen
        }

    }
}


