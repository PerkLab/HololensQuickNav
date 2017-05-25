using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    [Tooltip("Object you wish to rotate")]
    public GameObject selectedObject;
    private Transform childTransform;

    // Use this for initialization
    void Start () {
        Align();
	}

    public void Align()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.rotation = selectedObject.transform.rotation;
    }

    public void RotateRight () {
        selectedObject.transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));
        Align();
	}

    public void RotateLeft()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
        Align();
    }

    public void RotateUpS()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, 5.0f));
        Align();
    }

    public void RotateDownS()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, -5.0f));
        Align();
    }

    public void RotateUpR()
    {
        selectedObject.transform.Rotate(new Vector3(5.0f, 0.0f, 0.0f));
        Align();
    }

    public void RotateDownR()
    {
        selectedObject.transform.Rotate(new Vector3(-5.0f, 0.0f, 0.0f));
        Align();
    }
}
