using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 15 October 2025
 * Objective: Present a Win Condition for the Player so they know they have completed the level
 */

public class WinCondition : MonoBehaviour
{
    /// <summary>
    /// Detect Passage from the portal
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        print("Level Complete"); // Will be changed in the future, but for now will print that the level is completed in the console
    }
}
