using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * Author: [Ruffner, Kaylie]
 * Creation Date: [10-16-2025]
 * Summary: [This script handles the main menu canvas, buttons to switch to guide ui and to play the game]
 */
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Gets the button input
    /// </summary>
    public void PlayGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// This switches the scenes
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
