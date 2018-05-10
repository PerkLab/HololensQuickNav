using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Opening : MonoBehaviour {

    public GameObject cam;

    private bool Logo;
    private float timer;
    private bool IsRunning;

    static StreamWriter sw;


    // Use this for initialization
    void Awake () {

        GameObject.Find("Cursor").transform.Find("InteractiveMeshCursor").gameObject.SetActive(false);
        timer = 0f;
        Logo = true;
        IsRunning = true;
	}

    void Swap()
    {
        //make menu visible, hide logo
        GameObject.Find("Logo").SetActive(false);

        GameObject.Find("Cursor").transform.Find("InteractiveMeshCursor").gameObject.SetActive(true);
        GameObject.Find("Menu").transform.Find("Buttons").gameObject.SetActive(true);
        GameObject.Find("Menu").transform.Find("Background").gameObject.SetActive(true);
        GameObject.Find("Menu").transform.Find("Buttons").transform.position = cam.transform.position + cam.transform.forward * 1.5f;
        GameObject.Find("Menu").transform.Find("Background").transform.position = cam.transform.position + cam.transform.forward * 1.5f;




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
