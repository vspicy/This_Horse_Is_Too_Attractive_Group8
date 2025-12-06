using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Ruffner, Kaylie]
 * Creation Date: [11-7-2025]
 * Summary: [This script is the pause menu function, where the canvas will appear on screen when the character hits the 'esc' button]
 */

public class PauseFunction : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused;
    public bool howtoplay = false;
    public GameObject htpMenu;

    // Get reference to these two scripts right here to prevent certain inputs
    [SerializeField] private RestartLevel restart;
    [SerializeField] private FallFromWorld checkpointRestart;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1; // Always set the scale of time to standard on level startup
    }

    // Update is called once per frame
    void Update()
    {
        // gets the players key code for 'esc'
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // checks if its not paused
            if (!paused)
            {
                Pause();

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (paused) // Pressing Escape again will allow you to re-enter the game safely
            {
                Unpause();
            }
        }
    }
    // pauses the game
    void Pause()
    {
        // While paused, the player cannot restart
        restart.enabled = false;
        checkpointRestart.enabled = false;

        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    // unpauses the game
    void Unpause()
    {
        // While unpaused, the player can restart
        restart.enabled = true;
        checkpointRestart.enabled = true;

        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        htpMenu.SetActive(false);
        Cursor.visible = false;
        paused = false;
    }

    // how to play menu visibility
   public void HowToPlay()
    {
        if (howtoplay)
        {
            htpMenu.SetActive(false);
            howtoplay = false;
        }
        else
        {
            htpMenu.SetActive(true);
            howtoplay = true;
        }
    }
}
