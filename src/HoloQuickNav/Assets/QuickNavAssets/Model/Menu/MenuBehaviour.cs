using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

    public GameObject cam;
    public GameObject cursor;

	// Use this for initialization
	void OnEnable () {
        gameObject.transform.position = cam.transform.position + cam.transform.forward * 1.5f;
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
    }
}
