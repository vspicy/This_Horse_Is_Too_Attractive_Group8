using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Name: Ian Phurchpean, Hayden Bose
 * Date: 15 October 2025
 * Objective: Summary: When the player falls past a certain point, respawn them back at the designated respawn point and add to death count.
 */

public class FallFromWorld : MonoBehaviour
{
    // Get y coord threshold (designated kill level)
    public float killFloor;
    public Vector3 playerPosition;

    [Header("DeathCounter")]
    public TMP_Text deathCountDisplay;
    [SerializeField]
    private int deathCount;

    [Header("Checkpoints")]
    [SerializeField]
    List<GameObject> checkpoint;
    [SerializeField]
    Vector3 checkpointPos;


    void Start()
    {
        deathCount = 0;
        deathCountDisplay.text = "Death Count: " + deathCount.ToString();
    }

    // Call this update at fixed time intervals
    private void FixedUpdate()
    {
        if (transform.position.y < killFloor)
        {
            deathCount++; // Increment to the death counter
            deathCountDisplay.text = "Death Count: " + deathCount.ToString();
            transform.position = new Vector3(0f, 4f, -15f);

            transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            checkpointPos = other.transform.position;
            playerPosition = checkpointPos;
            Destroy(other.gameObject);
        }
    }

}
