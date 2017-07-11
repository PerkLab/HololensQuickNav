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

        GameObject.Find("Cursor").transform.FindChild("InteractiveMeshCursor").gameObject.SetActive(false);
        //GameObject.Find("InteractiveMeshCursor").transform.FindChild("CurosrRing").gameObject.SetActive(false);
        timer = 0f;
        Logo = true;
        IsRunning = true;
	}

    void Swap()
    {
        //make menu visible, hide logo
        GameObject.Find("Logo").SetActive(false);

        GameObject.Find("Cursor").transform.FindChild("InteractiveMeshCursor").gameObject.SetActive(true);
        //GameObject.Find("InteractiveMeshCursor").transform.FindChild("CurosrRing").gameObject.SetActive(true);
        GameObject.Find("Menu").transform.FindChild("Buttons").gameObject.SetActive(true);
        GameObject.Find("Menu").transform.FindChild("Background").gameObject.SetActive(true);
        GameObject.Find("Menu").transform.FindChild("Buttons").transform.position = cam.transform.position + cam.transform.forward * 1.5f;
        GameObject.Find("Menu").transform.FindChild("Background").transform.position = cam.transform.position + cam.transform.forward * 1.5f;
        //GameObject.Find("Menu").transform.LookAt(cam.transform.position);

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

    void CreateTextFile()
    {
        //copy over any existing text file
        sw = File.CreateText(Application.persistentDataPath + "/QuickNavLogData.txt");
        sw.AutoFlush = true;
        sw.WriteLine("QuickNav Log Data");
        sw.Write("Time of Test: ");
        sw.WriteLine(DateTime.Now);
        sw.WriteLine("-----------------------------");

    }
}
