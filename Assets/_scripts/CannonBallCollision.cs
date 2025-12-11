using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallCollision : MonoBehaviour
{
    public RepulsionTransform repulsionTransform;
    public Movement playerMovement;
    private TestAttraction[] allTestAttractions;
    public GameObject cannonBallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        allTestAttractions = FindObjectsOfType<TestAttraction>();
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
            DisableScripts();
            Invoke("EnableScripts", 1f);
        }
    }

    private void DisableScripts()
    {
        repulsionTransform.enabled = false;
        playerMovement.enabled = false;
        foreach (var t in allTestAttractions) t.enabled = false;
    }

    private void EnableScripts()
    {
        print("movement enabled");
        playerMovement.enabled = true;
        foreach (var t in allTestAttractions) t.enabled = true;
    }
}
