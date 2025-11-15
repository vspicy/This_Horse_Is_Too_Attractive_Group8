using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Name: Ian Phurchpean
 * Last Updated: 14 November 2025
 * Now I'm just pushing my buttons. HAAAAAAAAAAAAAAAA
*/

public class ButtonManager : MonoBehaviour
{
    public int sceneIndex; // Variable scene index based on level
    public int restartIndex; // Variable restart index based on level

    [Header("References and Game Objects")]
    [SerializeField]
    private Movement playerMovement;
    [SerializeField]
    private RestartLevel restart;
    public GameObject completeLevelUI;
    public GameObject UI;

    public void BackToMenu()
    {
        SceneManager.LoadScene(0); // Load Menu
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(sceneIndex); // Load chosen level
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(restartIndex); // Restart current Level

        playerMovement.enabled = true;
        restart.enabled = true;

        completeLevelUI.SetActive(false);
        UI.SetActive(true);
    }
}
