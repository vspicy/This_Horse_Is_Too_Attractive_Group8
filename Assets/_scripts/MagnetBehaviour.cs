using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 28 September 2025
 * Objective: Activate and deactivate the magnet system with button presses
 */


public class MagnetBehaviour : MonoBehaviour
{
    // Initialize Variables
    public bool magnetOn = false;

    [SerializeField] LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        Magnet();
    }

    /// <summary>
    /// Allow the player to activate and deactivate the magnet system
    /// </summary>

    private void Magnet()
    {
        // Magnet turns on
        if (Input.GetKey(KeyCode.E) && magnetOn == false)
        {
            magnetOn = true;
            print ("Magnet On"); // Magnet Active

            // Detect if the Magnet has hit a certain object
            if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out RaycastHit hitInfo, 20f, layerMask)) {
                Debug.Log("Hit Magnetic Object");
                Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.green);
            } else {
                Debug.Log("Did not hit Magnetic Object");
                Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);
            }
        }
        // Magnet turns off
        else if(Input.GetKey(KeyCode.Q) && magnetOn == true)
        {
            magnetOn = false;
            print ("Magnet Off"); // Magnet Inactive
        }
    }
}
