using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform firePoint;
    public Transform cannonCenter;
    public GameObject cannonBallPrefab;
    private Vector3 cannonBallVelocity;
    private float cannonFireRate = 0.8f;
    private float cannonBallStrength = 20;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CannonShoot", 0, cannonFireRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CannonShoot()
    {
        GameObject CannonBall = Instantiate(cannonBallPrefab, firePoint.position, Quaternion.identity);

        cannonBallVelocity = cannonCenter.position - firePoint.position;

        Rigidbody rb = CannonBall.GetComponent<Rigidbody> ();

        rb.AddForce(cannonBallVelocity * cannonBallStrength, ForceMode.Impulse);
    }
}
