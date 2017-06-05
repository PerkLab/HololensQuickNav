using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    [Tooltip("Object you wish to rotate")]
    public GameObject selectedObject;
    public GameObject cam;

    // Use this for initialization
    void Start () {
        Align();
	}

    void Update()
    {
        Align();
    }

    public void Align()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(cam.transform.position);
    }

    public void RotateRight () {
        selectedObject.transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));
	}

    public void RotateLeft()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
    }

    public void RotateUpS()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, 5.0f));
    }

    public void RotateDownS()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, -5.0f));
    }

    public void RotateUpR()
    {
        selectedObject.transform.Rotate(new Vector3(5.0f, 0.0f, 0.0f));
    }

    public void RotateDownR()
    {
        selectedObject.transform.Rotate(new Vector3(-5.0f, 0.0f, 0.0f));
    }
}
