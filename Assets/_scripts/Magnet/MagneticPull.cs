using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 28 October 2025
 * Objective: When Magnetic object is within the radius of this surface, attract it to them
 */

public class MagneticPull : MonoBehaviour
{
    // Initialize variables
    public float magnetRadius;
    public float magnetForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, magnetRadius))
        {
            Vector3 forceDir = transform.position - collider.transform.position;

            collider.GetComponent<Rigidbody>().AddForce(forceDir.normalized * magnetForce * Time.fixedDeltaTime);
        }
    }
}
