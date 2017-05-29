using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnSphere : MonoBehaviour {

    [Tooltip("Object you wish to rotate")]
    public GameObject selectedObject;
    public GameObject selectedCursor;
    public GameObject emptyObject;
    private Vector3 newForwardDir;
    private Vector3 oldForwardDir;
    private Vector3 rotation;

    // Use this for initialization
    void OnEnable () {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.LookAt(selectedCursor.transform);
        oldForwardDir = emptyObject.transform.forward;
    }

	// Update is called once per frame
	void Update () {
        
        emptyObject.transform.LookAt(selectedCursor.transform);
        newForwardDir = emptyObject.transform.forward;
        

        Quaternion q = Quaternion.FromToRotation(oldForwardDir, newForwardDir);
        selectedObject.transform.rotation = q * selectedObject.transform.rotation;
        //selectedObject.transform.Rotate(new Vector3(q.x, q.y, q.z));
        oldForwardDir = newForwardDir;
    }

}
