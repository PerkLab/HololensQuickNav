using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    [Tooltip("Object you wish to rotate")]
    public GameObject selectedObject;
    [Tooltip("Hololens camera")]
    public GameObject cam;

    void Start () {
        Align();
	}

    void Update()
    {
        Align();
    }

    public void Align()
    {
        // position the arrows around the selected object and face the user
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.FindChild("Model").transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(cam.transform.position);
    }

    // rotate the selected object by calling each funtion
    public void RotateRight () {
        selectedObject.transform.Rotate(new Vector3(0.0f, -3.0f, 0.0f));
	}

    public void RotateLeft()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 3.0f, 0.0f));
    }

    public void RotateUpS()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, 3.0f));
    }

    public void RotateDownS()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, -3.0f));
    }

    public void RotateUpR()
    {
        selectedObject.transform.Rotate(new Vector3(3.0f, 0.0f, 0.0f));
    }

    public void RotateDownR()
    {
        selectedObject.transform.Rotate(new Vector3(-3.0f, 0.0f, 0.0f));
    }
}
