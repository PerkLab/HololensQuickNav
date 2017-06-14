using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnGaze : MonoBehaviour
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

        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.FindChild("Model").transform.position.y, selectedObject.transform.position.z);
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

    public void RotateUpS(float amount)
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, amount));
    }

    public void RotateDownS(float amount)
    {
        selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, -amount));
    }

    public void RotateUpR(float amount)
    {
        selectedObject.transform.Rotate(new Vector3(amount, 0.0f, 0.0f));
    }

    public void RotateDownR(float amount)
    {
        selectedObject.transform.Rotate(new Vector3(-amount, 0.0f, 0.0f));
    }
}
