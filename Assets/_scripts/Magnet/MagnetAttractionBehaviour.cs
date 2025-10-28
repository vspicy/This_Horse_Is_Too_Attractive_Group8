using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 26 October 2025
 * Objective: When the player activates the magnet, pull player towards a magnetic object
 */

public class MagnetAttractionBehaviour : MonoBehaviour
{
    // Initialize Variables
    [Header("Player Components")]
    public Transform player;
    public SphereCollider magnetRange;
    public GameObject magnetRadius;
    private Rigidbody rb;

    [Header("Magnetism")]
    public float attractRange;
    public float attractIntensity;
    public float distanceToPlayer;
    private Vector3 pullForce;

    // Start is called before the first frame update
    void Start()
    {
        // Get components of the player and the magnet radius
        rb = player.GetComponent<Rigidbody>();
        magnetRadius = magnetRange.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
