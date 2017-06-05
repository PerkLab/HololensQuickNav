using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithHead : MonoBehaviour {

    [Tooltip("The game object this object will follow : Main Camera by default")]
    public GameObject ReferenceObject;

    public GameObject selectedObject;

    private bool IsRunning = false;


    // the position different between the objects position and the reference object's transform.forward
    private Vector3 mOffsetDirection;

    // this object's direction
    private Vector3 mDirection;

    // the offset rotation at start
    private Quaternion mOffsetRotation;

    private Vector3 mNormalzedOffsetDirection;

    public bool RotateA = false;
    public bool RotateS = false;
    public bool RotateR = false;



    /// <summary>
    /// Initialize vectors
    /// </summary>
    private void OnEnable()
    {
        mOffsetDirection = selectedObject.transform.position - ReferenceObject.transform.position;
        mDirection = ReferenceObject.transform.forward.normalized;
        mNormalzedOffsetDirection = mOffsetDirection.normalized;
        mOffsetRotation = Quaternion.FromToRotation(mDirection, mNormalzedOffsetDirection);
        IsRunning = true;
    }


    public void Align()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(ReferenceObject.transform);
        //update vector from users head to model
        mOffsetDirection = selectedObject.transform.position - ReferenceObject.transform.position;
    }

    /// <summary>
    /// Animate!
    /// </summary>
    protected virtual void Update()
    {
        if (IsRunning)
        {
            Vector3 newDirection = ReferenceObject.transform.forward;
            //newDirection = Vector3.Normalize(mOffsetRotation * ReferenceObject.transform.forward);
            newDirection = Vector3.Normalize(ReferenceObject.transform.forward);


            float angle = Vector3.Angle(mOffsetDirection, newDirection);
            Vector3 polarity = Vector3.Cross(mOffsetDirection, newDirection);
            float distance = Vector3.Distance(selectedObject.transform.position, ReferenceObject.transform.position);
            float minAngle = 5f - 0.1f * distance;

            if(polarity.y > 0)
            {
                if(angle > minAngle)
                {
                    if (RotateA) //RotateA
                    { selectedObject.transform.Rotate(new Vector3(0.0f, -angle * 0.1f, 0.0f)); }
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
                    { selectedObject.transform.Rotate(new Vector3(0.0f, angle * 0.1f, 0.0f)); }
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

