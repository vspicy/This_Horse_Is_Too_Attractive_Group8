using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bose, Hayden]
 * Creation Date: [12-9-2025]
 * Summary: This script handles deleting the cannon ball after 8 seconds of existance
 */
public class CannonBall : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Runs "Delete cannonBall 8 seconds after cannon ball spawns"
        Invoke("DeleteCannonBall", 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Deletes cannon ball
    private void DeleteCannonBall()
    {
        Destroy(gameObject);
    }
}
