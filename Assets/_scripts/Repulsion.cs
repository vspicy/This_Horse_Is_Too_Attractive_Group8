using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Ruffner, Kaylie]
 * Creation Date: [10-22-2025]
 * Summarization: [This script handles the repulsion for the player, where the player can repel themselves.]
 */
public class Repulsion : MonoBehaviour
{
    private float repulsionForce = 10f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // call when want to trigger the repulsion
    public void PlayerRepulsion(Vector3 sourcePosition)
    {
        // calculate direction away from the source 
        Vector3 direction = (transform.position - sourcePosition).normalized;

        //clear any downward velocity if a clean launch is wanted
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //apply the impluse force
        rb.AddForce(direction * repulsionForce, ForceMode.Impulse);
    }
}
