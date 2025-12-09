using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeleteCannonBall", 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeleteCannonBall()
    {
        Destroy(gameObject);
    }
}
