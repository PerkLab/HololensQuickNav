using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{

    [Tooltip("Object you wish to rotate")]
    public GameObject selectedObject;
    public GameObject cam;

    // Use this for initialization
    void Start()
    {
        Align();
    }

    void Update()
    {
        Align();
    }

    public void Align()
    {

        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(cam.transform);
    }


    public void RotateRight(float amount)
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, -amount, 0.0f));
    }

    public void RotateLeft(float amount)
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, amount, 0.0f));
    }

    public void RotateUpS()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, 0.3f));
    }

    public void RotateDownS()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, -0.3f));
    }

    public void RotateUpSMinute()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, 0.1f));
    }

    public void RotateDownSMinute()
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, -0.1f));
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
