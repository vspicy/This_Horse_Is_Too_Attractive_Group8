using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 28 September 2025
 * Objective: Activate and deactivate the magnet system with button presses
 */


public class PlayerBehaviour : MonoBehaviour
{
    // Initialize variables
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerCtrl;
    public Rigidbody rb;

    public float rotSpd;

    [Header("Camera")]
    public CameraStyle currentStyle;
    public Transform combatLookAt;

    public enum CameraStyle
    {
        Base,
        Combat
    }

    // Start is called before the first frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide Cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Find and rotate orientation
        //Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
       // orientation.forward = viewDir.normalized;

        // Rotate player
       // if (currentStyle == CameraStyle.Base)
       // {
            //float horzInput = Input.GetAxis("Horizontal");
           // float vertInput = Input.GetAxis("Vertical");
           // Vector3 inputDir = orientation.forward * vertInput + orientation.right * horzInput;

          //  if (inputDir != Vector3.zero)
          //  {
           //     playerCtrl.forward = Vector3.Slerp(playerCtrl.forward, inputDir.normalized, Time.deltaTime * rotSpd);
          //  }
       // }
        
        // Offset camera and rotate player
       // else if (currentStyle == CameraStyle.Combat)
       // {
         //   Vector3 dirToCombatLookAt = player.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
         //   orientation.forward = dirToCombatLookAt.normalized;

          //  player.forward = dirToCombatLookAt.normalized;
       // }
    }
    
    private void FixedUpdate()
    {
        // Find and rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // Rotate player
        if (currentStyle == CameraStyle.Base)
        {
            float horzInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * vertInput + orientation.right * horzInput;

            if (inputDir != Vector3.zero)
            {
                playerCtrl.forward = Vector3.Slerp(playerCtrl.forward, inputDir.normalized, Time.deltaTime * rotSpd);
            }
        }

        // Offset camera and rotate player
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = player.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            player.forward = dirToCombatLookAt.normalized;
        }
    } 
}
