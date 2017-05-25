using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftDirection : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;
    private Transform childTransform;

    // Use this for initialization
    void Start()
    {
        Align();
    }

    public void Align()
    {
        gameObject.transform.localPosition = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.rotation = selectedObject.transform.rotation;
    }


    public void ArrowRight() {
        selectedObject.transform.localPosition += new Vector3(0.01f, 0.0f, 0.0f);
        Align();
	}

    public void ArrowLeft()
    {
        selectedObject.transform.localPosition -= new Vector3(0.01f, 0.0f, 0.0f);
        Align();
    }

    public void ArrowUp()
    {
        selectedObject.transform.localPosition += new Vector3(0.0f, 0.01f, 0.0f);
        Align();
    }

    public void ArrowDown()
    {
        selectedObject.transform.localPosition -= new Vector3(0.0f, 0.01f, 0.0f);
        Align();
    }
    public void ArrowForward()
    {
        selectedObject.transform.localPosition -= new Vector3(0.0f, 0.0f, 0.01f);
        Align();
    }

    public void ArrowBack()
    {
        selectedObject.transform.localPosition += new Vector3(0.0f, 0.0f, 0.01f);
        Align();
    }

}
