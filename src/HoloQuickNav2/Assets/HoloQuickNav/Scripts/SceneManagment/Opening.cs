using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls behaviour of logo when application is first opened
/// </summary>
public class Opening : MonoBehaviour {

    /// <summary> Hololens camera in scene </summary>
    public GameObject cam;
    private float timer;
    private bool IsRunning;

    // Use this for initialization
    void Awake () {

        timer = 0f;
        IsRunning = true;
	}

    void Swap()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
	
	// Update is called once per frame
	void Update () {
        if(IsRunning)
        {
            //display logo directly infront of the user as they move their head
            if (timer < 3f)
            {
                timer += Time.deltaTime;
                Vector3 newPosition = cam.transform.position + cam.transform.forward * 1.5f;
                GameObject.Find("Logo").transform.position = Vector3.Slerp(GameObject.Find("Logo").transform.position, newPosition, 0.1f);
                GameObject.Find("Logo").transform.LookAt(2 * GameObject.Find("Logo").transform.position - cam.transform.position);
            }
            //after 3 second swap scenes to the main menu
            else
            {
                Swap();
                IsRunning = false;
            }
        }
        
	}

}
