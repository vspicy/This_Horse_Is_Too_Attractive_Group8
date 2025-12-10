using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallCollision : MonoBehaviour
{
    public RepulsionTransform repulsionTransform;
    public GameObject cannonBallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            print("Player has hit cannon ball");
            MovementManagement();
        }
    }

    private void MovementManagement()
    {
        repulsionTransform.enabled = false;
    }

    private void TurnOnMovement()
    {
        repulsionTransform.enabled = true;
    }
}
