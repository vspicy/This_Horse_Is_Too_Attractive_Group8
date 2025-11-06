using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Ruffner, Kaylie]
 * Date of Creation :[11-5-2025]
 * Summary: [This script handles switching to the level complete scene when the player collides with the box collider.]
 */
public class LevelComplete : MonoBehaviour
{
    public int sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this is for whent he player collides with the box collider and switches scene
    private void OnTriggerEnter(Collider other)
    {
        // check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            // load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
