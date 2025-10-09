using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Name: Ian Phurchpean
 * Date: 3 October 2025
 * Objective: Code that allows the player to attract themselves to other objects with a magnet.
 */


public class MagnetPull : MonoBehaviour
{
    // Initialize Variables

    private Rigidbody rb;

    [Header("References")]
    public Movement pm;
    public Transform cam;
    public Transform magnetTip;
    public LayerMask canMagnet;
    public LineRenderer lr;

    [Header("Attraction")]
    public float maxMagnetDistance;
    public float maxMagnetDelay;
    public float overshoot;

    private Vector3 magnetPoint;

    [Header("Cooldown")]
    public float magnetCd;
    private float magnetTimer;

    [Header("Input")]
    public KeyCode magnetKey = KeyCode.Mouse0;

    private bool isAttracted;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Movement>(); // Get Component of the Player's own Movement.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(magnetKey) && isAttracted == false) StartMagnet();
        if (magnetTimer > 0)
            magnetTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (isAttracted)
            lr.SetPosition(0, magnetTip.position);
    }

    // Magnet Functions

    /// <summary>
    /// Start the magnet.
    /// </summary>
    private void StartMagnet()
    {
        if (magnetTimer > 0) return;

        print("Magnet Active");

        isAttracted = true; // The player is magnetized
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxMagnetDistance, canMagnet))
        {
            magnetPoint = hit.point;

            Invoke(nameof(ExecuteMagnet), magnetCd);
        }
        else
        {
            magnetPoint = cam.position + cam.forward * maxMagnetDistance;
            Invoke(nameof(StopMagnet), magnetCd);
        }

        lr.enabled = true;
        lr.SetPosition(1, magnetPoint);
    }

    /// <summary>
    /// Magnet is active, begin to pull.
    /// </summary>
    private void ExecuteMagnet()
    {
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float magnetRelativeY = magnetPoint.y - lowestPoint.y;
        float arcPeak = magnetRelativeY + overshoot;

        if (magnetRelativeY < 0) arcPeak = overshoot;

        pm.JumpToPosition(magnetPoint, arcPeak);
        Invoke(nameof(StopMagnet), 1f);
    }

    /// <summary>
    /// Stop the magnet.
    /// </summary>
    public void StopMagnet()
    {
        isAttracted = false; // Set Attraction Properties off
        print("Magnet Inactive");

        magnetTimer = magnetCd;

        lr.enabled = false; // disable line (testing)
    }
}
