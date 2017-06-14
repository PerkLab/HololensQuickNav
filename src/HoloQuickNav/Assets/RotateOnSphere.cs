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

    private bool IsRunning = false;
    private bool HasGaze = false;

    private float timer = 0f; 
    private float delay = 0.1f;

    // Use this for initialization
    void OnEnable () {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.LookAt(selectedCursor.transform);
        oldForwardDir = emptyObject.transform.forward;
    }

    void Setup()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.LookAt(selectedCursor.transform);
        oldForwardDir = emptyObject.transform.forward;
    }

    public void OnGaze(bool gaze)
    {
        HasGaze = gaze; 
    }

    public void StartRotation()
    {
        Setup();
        if (HasGaze)
        {
            IsRunning = true;
        }
        GameObject.Find("CommandText").transform.FindChild("CommandName").GetComponent<TextMesh>().text = "Rotate Free > Start";
    }

    public void PauseRotation()
    {
        IsRunning = false;
        GameObject.Find("CommandText").transform.FindChild("CommandName").GetComponent<TextMesh>().text = "Rotate Free > Pause";
    }
    
    // Update is called once per frame
	void Update () {
        if (HasGaze)
        {
            if (IsRunning)
            {
                if (timer < delay) //delay when running if user moves their gaze onto sphere
                {
                    timer += Time.deltaTime;
                    Setup();
                }
                else
                {
                    Rotate();
                }  
            }
        }
        else //user takes gaze off sphere
        {
            timer = 0f; 
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
