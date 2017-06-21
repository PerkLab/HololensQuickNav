using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftDirection : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;
    public GameObject cam;

    // Use this for initialization
    void OnEnable()
    {
        Align();
        gameObject.transform.LookAt(cam.transform.position);
    }

    public void Align()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
    }

    public void ArrowRight(float amount)
    {
        Vector3 direction = Quaternion.Euler(0, 90, 0) * gameObject.transform.forward.normalized;
        selectedObject.transform.position += direction * amount;
        Align();
    }
    public void ArrowLeft(float amount)
    {
        Vector3 direction = Quaternion.Euler(0, -90, 0) * gameObject.transform.forward.normalized;
        selectedObject.transform.position += direction * amount;
        Align();
    }
    public void ArrowUp(float amount)
    {
        selectedObject.transform.position += new Vector3(0.0f, amount, 0.0f);
        Align();
    }
    public void ArrowDown(float amount)
    {
        selectedObject.transform.position -= new Vector3(0.0f, amount, 0.0f);
        Align();
    }
    public void ArrowForward(float amount)
    {
        Vector3 direction = Quaternion.Euler(0, 180, 0) * gameObject.transform.forward.normalized;
        selectedObject.transform.position += direction * amount;
        Align();
    }
    public void ArrowBack(float amount)
    {
        Vector3 direction = gameObject.transform.forward.normalized;
        selectedObject.transform.position += direction * amount;
        Align();
    }
}
