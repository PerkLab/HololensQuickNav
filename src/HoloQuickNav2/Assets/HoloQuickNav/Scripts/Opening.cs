using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour {

    public GameObject cam;
    private float timer;
    private bool IsRunning;

    static StreamWriter sw;


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
            if (timer < 3f)
            {
                timer += Time.deltaTime;
                Vector3 newPosition = cam.transform.position + cam.transform.forward * 1.5f;
                GameObject.Find("Logo").transform.position = Vector3.Slerp(GameObject.Find("Logo").transform.position, newPosition, 0.1f);
                GameObject.Find("Logo").transform.LookAt(2 * GameObject.Find("Logo").transform.position - cam.transform.position);
            }
            else
            {
                Swap();
                IsRunning = false;
            }
        }
        
	}

}
