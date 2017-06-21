using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommandText : MonoBehaviour {

    [Tooltip("Hololens Camera")]
    public GameObject cam;
    [Tooltip("Object to position text around")]
    public GameObject selectedObject;
	
	void Update () {
        //position around selected object and face camera
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(2 * gameObject.transform.position - cam.transform.position);
    }
}
