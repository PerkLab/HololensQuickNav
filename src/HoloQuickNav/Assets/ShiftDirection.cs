using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftDirection : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;
    private Transform childTransform;

    // Use this for initialization
    void OnEnable()
    {
        Align();
    }
    void Start()
    {
        Align();
    }

    public void Align()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        //gameObject.transform.rotation = selectedObject.transform.rotation;
    }

    public void ArrowRight() {
        Vector3 direction = Quaternion.Euler(0, 90, 0) * gameObject.transform.forward.normalized;
        selectedObject.transform.position += direction * 0.01f;
        Align();
    }
    public void ArrowLeft()
    {
        Vector3 direction = Quaternion.Euler(0, -90, 0) * gameObject.transform.forward.normalized;
        selectedObject.transform.position += direction * 0.01f;
        Align();
    }
    public void ArrowUp()
    {
        selectedObject.transform.position += new Vector3(0.0f, 0.01f, 0.0f);
        Align();
    }
    public void ArrowDown()
    {
        selectedObject.transform.position -= new Vector3(0.0f, 0.01f, 0.0f);
        Align();
    }
    public void ArrowForward()
    {
        Vector3 direction = Quaternion.Euler(0, 180, 0) * gameObject.transform.forward.normalized;
        selectedObject.transform.position += direction * 0.01f;
        Align();
    }
    public void ArrowBack()
    {
        Vector3 direction = gameObject.transform.forward.normalized;
        selectedObject.transform.position += direction * 0.01f;
        Align();
    }

}
