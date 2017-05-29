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
    private float delay = 0.5f;
    private float timer = 0;

    // Use this for initialization
    void OnEnable () {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.LookAt(selectedCursor.transform);
        oldForwardDir = emptyObject.transform.forward;
    }


	// Update is called once per frame
	void Update () {
        if(timer < delay)
        {
            timer += Time.deltaTime;

            emptyObject.transform.LookAt(selectedCursor.transform);
            newForwardDir = emptyObject.transform.forward;

            Quaternion q = Quaternion.FromToRotation(oldForwardDir, newForwardDir);
            oldForwardDir = newForwardDir;

        }
        else
        {
            Rotate();
        }
        
    }

    void Rotate()
    {
        emptyObject.transform.LookAt(selectedCursor.transform);
        newForwardDir = emptyObject.transform.forward;

        Quaternion q = Quaternion.FromToRotation(oldForwardDir, newForwardDir);
        selectedObject.transform.rotation = q * selectedObject.transform.rotation;
        oldForwardDir = newForwardDir;
    }

}
