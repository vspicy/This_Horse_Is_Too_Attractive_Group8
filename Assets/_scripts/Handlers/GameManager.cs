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
    public float restart = 0f;
    public GameObject completeLevelUI;
    public GameObject UI;
    
    [Header("References")]
    public Timer timer;
    [SerializeField] private Movement playerMovement;
    public TMP_Text finalTime; // Get reference to the Final Time text object
    public int sceneToLoad;

    public int restartScene;
    public int backToMenu;
    public GameObject horse;

    /// <summary>
    /// Play when the Level Completes
    /// </summary>
    public void LevelComplete()
    {
        Debug.Log("Level Complete");
        completeLevelUI.SetActive(true);
        UI.SetActive(false);

        timer.playing = false;
        finalTime.text = "Time: " + timer.minutes.ToString("00") + ":" + timer.seconds.ToString("00") + "." + timer.milliseconds.ToString("00"); // Show the player's final time when the stopwatch ends

        // Disable the Movement script because the level is complete
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.R)) // Restart Level
        {
            SceneManager.LoadScene(restartScene);
            completeLevelUI.SetActive(false);
            UI.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Load Menu
        {
            SceneManager.LoadScene(backToMenu);
        }

        if (Input.GetKeyDown(KeyCode.Return)) // Load Next Level
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        if (Input.GetKeyDown(KeyCode.H)) // Just do this
        {
            horse.SetActive(true);
        }
    }
            
}
