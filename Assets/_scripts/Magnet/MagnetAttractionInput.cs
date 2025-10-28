using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 24 October 2025
 * Objective: Code that allows the player to attract themselves to other objects with a magnet (but Updated).
 */

public class MagnetAttractionInput : MonoBehaviour
{
    public GameObject magnetRadius;

    // Update every frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("LMB Down: Magnet Active");
            magnetRadius.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("LMB Up: Magnet Inactive");
            magnetRadius.SetActive(false);
        }
    }
}
