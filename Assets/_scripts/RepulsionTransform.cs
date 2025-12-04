using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionTransform : MonoBehaviour
{
    private float moveSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        print("Transform Active");
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }
}
