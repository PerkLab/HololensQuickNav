using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnSphere : MonoBehaviour {

    [Tooltip("Object you wish to rotate")]
    public GameObject selectedObject;
    [Tooltip("Cursor to follow on sphere")]
    public GameObject selectedCursor;
    [Tooltip("Assign empty object to position on selected object")]
    public GameObject emptyObject;

    //variables to track movement in cursor
    private Vector3 newForwardDir;
    private Vector3 oldForwardDir;
    private Vector3 rotation;

    //if command is running
    private bool IsRunning = false;
    //if user's gaze is on sphere
    private bool HasGaze = false;

    //timer for start up to allow smoother inizialization 
    private float timer = 0f; 
    private float delay = 0.1f;


    void OnEnable () {
        Setup();
    }

    void Setup()
    {
        //position empty object and sphere around selected object
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        emptyObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        //rotate empty object to look at cursor, forward axis creates vector between center of model and the cursor location on the sphere
        emptyObject.transform.LookAt(selectedCursor.transform);
        //save the current direction of the forward axis
        oldForwardDir = emptyObject.transform.forward;
    }

    public void OnGaze(bool gaze)
    {
        //declare true/false if user's gaze is on/off the sphere
        HasGaze = gaze; 
    }

    public void StartRotation() //user calls 'Start' voice command
    {
        Setup();
        if (HasGaze) //if user's gaze is on sphere, start rotation
        {
            IsRunning = true;
        }
        //update command text for current command
        GameObject.Find("CommandText").transform.Find("CommandName").GetComponent<TextMesh>().text = "Rotate Free > Start";
    }

    public void PauseRotation() //user calls 'Pause' voice command
    {
        //stop rotation
        IsRunning = false;
        //update command text for current command
        GameObject.Find("CommandText").transform.Find("CommandName").GetComponent<TextMesh>().text = "Rotate Free > Pause";
    }
    

	void Update () {
        if (HasGaze)
        {
            if (IsRunning) //if command is running and user's gaze is on sphere
            {
                if (timer < delay) //delay when running if user moves their gaze onto sphere for smoother start
                {
                    timer += Time.deltaTime;
                    Setup();
                }
                else
                {
                    Rotate(); //after delay start rotation
                }  
            }
        }
        else //user takes gaze off sphere, reset timer
        {
            timer = 0f; 
        }
    }

    void Rotate()
    {
        //update forward direction of empty object, save new direction
        emptyObject.transform.LookAt(selectedCursor.transform);
        newForwardDir = emptyObject.transform.forward;

        //calculate rotation between old and new direction
        Quaternion q = Quaternion.FromToRotation(oldForwardDir, newForwardDir);
        //apply rotation to selected object
        selectedObject.transform.rotation = q * selectedObject.transform.rotation;
        //update directions
        oldForwardDir = newForwardDir;
    }

}
