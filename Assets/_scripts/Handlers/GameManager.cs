using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/*
 * Name: Ian Phurchpean
 * Last Updated: 14 November 2025
 * In-game UI Manager to make it easier to show final time and move to new levels
*/

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    public GameObject UI;
    
    [Header("References")]
    public Timer timer;
    [SerializeField] private Movement playerMovement;
    [SerializeField] private RestartLevel restart;
    public TMP_Text finalTime; // Get reference to the Final Time text object

    /// <summary>
    /// Play when the Level Completes
    /// </summary>
    public void LevelComplete()
    {
        Debug.Log("Level Complete");
        completeLevelUI.SetActive(true);
        UI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        timer.playing = false;
        finalTime.text = "Time: " + timer.minutes.ToString("00") + ":" + timer.seconds.ToString("00") + "." + timer.milliseconds.ToString("00"); // Show the player's final time when the stopwatch ends

        // Disable the Movement script because the level is complete
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            restart.enabled = false;
        }
    }
            
}
