using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour {

    public GameObject cam;
    public GameObject selectedObject;

    private Vector3 lastDirectionCam;
    private Vector3 currentDirectionCam;
    private Vector3 lastDirectionObject;
    private Vector3 currentDirectionObject;
    private bool IsRunning = false;
    public GameObject emptyObject;

    // Use this for initialization
	void OnEnable () {
        lastDirectionCam = cam.transform.forward;
        emptyObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.LookAt(emptyObject.transform.position + selectedObject.transform.up.normalized);
        lastDirectionObject = emptyObject.transform.forward;
        IsRunning = true;
	}
	
	// Update is called once per frame
	void Update () {

        currentDirectionCam = cam.transform.forward;

        if (IsRunning)
        {
            Quaternion camRotation = Quaternion.FromToRotation(lastDirectionCam, currentDirectionCam);
            emptyObject.transform.rotation = camRotation * emptyObject.transform.rotation;
            //currentDirectionObject = emptyObject.transform.forward;
            //Quaternion objectRotation = Quaternion.FromToRotation(lastDirectionObject, currentDirectionObject);
            //selectedObject.transform.rotation = camRotation * selectedObject.transform.rotation;
            lastDirectionObject = currentDirectionObject;
        }
        lastDirectionCam = currentDirectionCam;
        
	}
}
