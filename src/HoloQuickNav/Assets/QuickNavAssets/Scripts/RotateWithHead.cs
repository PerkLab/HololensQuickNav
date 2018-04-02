using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithHead : MonoBehaviour {

    [Tooltip("Hololens camera that the rotation will follow")]
    public GameObject ReferenceObject;
    [Tooltip("Object you wish to rotate")]
    public GameObject selectedObject;

    private bool IsRunning = false;

    // the position different between the objects position and the reference object's transform.forward
    private Vector3 mOffsetDirection;

    // this object's direction
    private Vector3 mDirection;

    // the offset rotation at start
    private Quaternion mOffsetRotation;

    // this object's normalized direction
    private Vector3 mNormalzedOffsetDirection;

    // allows developer to select axis of rotation
    public bool RotateA = false;
    public bool RotateS = false;
    public bool RotateR = false;

    /// <summary>
    /// Initialize vectors
    /// </summary>
    private void OnEnable()
    {
        //calculate initial offset distances, directions
        mOffsetDirection = selectedObject.transform.position - ReferenceObject.transform.position;
        mDirection = ReferenceObject.transform.forward.normalized;
        mNormalzedOffsetDirection = mOffsetDirection.normalized;
        mOffsetRotation = Quaternion.FromToRotation(mDirection, mNormalzedOffsetDirection);
        IsRunning = true;
    }


    public void Align()
    {
        //position arrows aroud selected object and face user
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.Find("Model").transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(ReferenceObject.transform);
        //update vector from users head to centre of model
        //allows user to move around the object without effecting rotation inadvertently 
        mOffsetDirection = selectedObject.transform.position - ReferenceObject.transform.position;
    }

    public void StartRunning()
    {
        IsRunning = true;
    }

    public void StopRunning()
    {
        IsRunning = false;
    }

    /// <summary>
    /// Animate!
    /// </summary>
    protected virtual void Update()
    {
        if (IsRunning)
        {
            //calculate normalized direction from camera
            Vector3 newDirection = ReferenceObject.transform.forward;
            newDirection = Vector3.Normalize(ReferenceObject.transform.forward);

            //calculate the angle between the user's gaze and direct vector from user to the model's center
            float angle = Vector3.Angle(mOffsetDirection, newDirection);
            //calculate cross product to get polarity/direction of angle
            Vector3 polarity = Vector3.Cross(mOffsetDirection, newDirection);
            //calculate a minimum angle the user must turn to cause the model to rotate
            //this angle is dependent on the user's distance from the object
            float distance = Vector3.Distance(selectedObject.transform.position, ReferenceObject.transform.position);
            float minAngle = 5f - 0.1f * distance;

            if(polarity.y > 0) //positive polarity.y
            {
                if(angle > minAngle)
                {
                    if (RotateA) //RotateA
                    { selectedObject.transform.Rotate(new Vector3(0.0f, angle * 0.1f, 0.0f)); }
                    else if (RotateS) //RotateS
                    { selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, -angle * 0.1f)); }
                    else if (RotateR) //RotateR
                    { selectedObject.transform.Rotate(new Vector3(-angle * 0.1f, 0.0f, 0.0f)); }
                }
            }
            else //negative polarity.y
            {
                if(angle > minAngle)
                {
                    if (RotateA) //RotateA
                    { selectedObject.transform.Rotate(new Vector3(0.0f, -angle * 0.1f, 0.0f)); }
                    else if (RotateS) //RotateS
                    { selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, angle * 0.1f)); }
                    else if (RotateR)//RotateR
                    { selectedObject.transform.Rotate(new Vector3(angle * 0.1f, 0.0f, 0.0f)); }
                }
            }

            Align();

        }
    }
}

