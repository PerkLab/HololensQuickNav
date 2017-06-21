using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftEyeRotation : MonoBehaviour
{
    [Tooltip("Object you wish to rotate")]
    public GameObject selectedObject;
    [Tooltip("Hololens camera")]
    public GameObject cam;

    //distances for monitoring head movements 
    private float currentDistance;
    private float lastDistance;
    //amount to rotate the object
    private float amount;

    public bool IsRunning = false;

    //empty variables for axis vector and marker locations 
    private Vector3 axis;
    private Vector3 RightMarker;
    private Vector3 NoseMarker;

    // Use this for initialization
    void OnEnable()
    {
        //calculate distance between camera and model
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        //assign positions of markers to variables
        RightMarker = selectedObject.transform.FindChild("SubFrame/RightMarker").transform.position;
        NoseMarker = selectedObject.transform.FindChild("SubFrame/NoseMarker").transform.position;
        //calculate axis vector
        axis = new Vector3(RightMarker.x - NoseMarker.x, RightMarker.y - NoseMarker.y, RightMarker.z - NoseMarker.z);
        //enable rotation
        IsRunning = true;
    }

    public void StartRotation()
    {
        //update distance and enable rotation
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        IsRunning = true;
    }

    public void PauseRotation()
    {
        //disable rotation
        IsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            currentDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);

            //if user moves closer, rotate in positive direction
            if (currentDistance < lastDistance)
            {
                amount = Mathf.Abs(currentDistance - lastDistance) * 500f;
                selectedObject.transform.RotateAround(NoseMarker, axis, amount);
    
            }
            //if user moves farther away, rotate in negative direction
            else if (currentDistance > lastDistance)
            {
                amount = Mathf.Abs(currentDistance - lastDistance) * 500f;
                selectedObject.transform.RotateAround(NoseMarker, axis, -amount);
            }
            else
            {
                //do nothing if user hasn't moved thier head
            }

            //update distance
            lastDistance = currentDistance;
        }

        
    }
}
