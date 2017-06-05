using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

    public GameObject cam;
    public GameObject cursor;
    public bool HelpWindow;
    public bool Menu;

	// Use this for initialization
	void OnEnable () {
        if(Menu)
        {
            gameObject.transform.position = cam.transform.position + cam.transform.forward * 1.5f;
        }
        else if(HelpWindow)
        {
            Vector3 direction = Quaternion.Euler(0f, 20f, 0f) * cam.transform.forward;
            gameObject.transform.position = cam.transform.position + direction * 1.5f;
        }

        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
    }
}
