using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [12-9-2025]
 * Summary: This script handles the cannon ball collision with player logic
 */
public class CannonBallCollision : MonoBehaviour
{
    public RepulsionTransform repulsionTransform;
    public Movement playerMovement;
    private TestAttraction[] allTestAttractions;
    public GameObject cannonBallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Grabs references for all attraction scripts in scene
        allTestAttractions = FindObjectsOfType<TestAttraction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //When cannon ball hits player, run "disable scripts" and then run "enable scripts" 1 second later
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            print("Player has hit cannon ball");
            DisableScripts();
            Invoke("EnableScripts", 1f);
        }
    }

    //Disable scripts that would get in the way of the cannon ball knocking the player away
    private void DisableScripts()
    {
        repulsionTransform.enabled = false;
        playerMovement.enabled = false;
        foreach (var t in allTestAttractions) t.enabled = false;
    }

    //Re-enable disabled scripts
    private void EnableScripts()
    {
        print("movement enabled");
        playerMovement.enabled = true;
        foreach (var t in allTestAttractions) t.enabled = true;
    }
}
