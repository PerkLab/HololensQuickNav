using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommandText : MonoBehaviour {

    public GameObject cam;
    public GameObject selectedObject;
    //private float speed = 0.5f;

    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        //gameObject.transform.position = Vector3.Slerp(gameObject.transform.position,(cam.transform.position + cam.transform.forward * 1.5f), speed*Time.deltaTime);
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);


    }
}
