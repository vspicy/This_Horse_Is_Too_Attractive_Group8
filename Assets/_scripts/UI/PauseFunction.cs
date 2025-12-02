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

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        paused = false;
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
            }
            else
            {
                Unpause();
            }
        }
    }
    // pauses the game
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    // unpauses the game
    void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        paused = false;
    }
}
