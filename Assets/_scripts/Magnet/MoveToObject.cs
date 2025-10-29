using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 28 October 2025
 * Objective: When a magnetic surface is close to the player, attract them to it.
 */

public class MoveToObject : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private GameObject magnetSurface; // Make private variable visible
    [SerializeField] private float velocity;

    [Header("Public")]
    public MagnetAttraction magnetOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (magnetOn == true)
            transform.position = Vector3.MoveTowards(this.transform.position, magnetSurface.transform.position, velocity * Time.deltaTime);
        else
        {
            return;
        }
    }
}
