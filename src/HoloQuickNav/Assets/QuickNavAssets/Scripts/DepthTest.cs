using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthTest : MonoBehaviour
{

    [Tooltip("The game object this object will follow : Main Camera by default")]
    public GameObject ReferenceObject;

    public GameObject selectedObject;

    [Tooltip("Auto start? or status")]
    public bool IsRunning = false;

    [Tooltip("translation speed : higher is faster")]
    public float LerpPositionSpeed = 1f;

    [Tooltip("rotation speed : higher is faster")]
    public float LerpRotationSpeed = 0.5f;

    [Tooltip("Does not center the object to the reference object's transform.forward vector")]
    public bool KeepStartingOffset = true;

    [Tooltip("Force the object to keep relative to the reference object's transform.forward")]
    public bool KeepInFront = true;

    // the position different between the objects position and the reference object's transform.forward
    private Vector3 mOffsetDirection;

    // this object's direction
    private Vector3 mDirection;

    // the offset rotation at start
    private Quaternion mOffsetRotation;

    // the offset distance at start
    private float mOffsetDistance = 0;

    private Vector3 mNormalzedOffsetDirection;

    private Vector3 axisDirection;
    private float timer = 0;

    /// <summary>
    /// set the reference object if not set already
    /// </summary>
    private void OnEnable()
    {
        timer = 0;
        StartRunning();
    }

    // start the object following the reference object
    public void StartRunning()
    {
        PlaceAxis();

        mOffsetDirection = selectedObject.transform.position - ReferenceObject.transform.position;
        mOffsetDistance = mOffsetDirection.magnitude;
        mDirection = ReferenceObject.transform.forward.normalized;
        mNormalzedOffsetDirection = mOffsetDirection.normalized;
        mOffsetRotation = Quaternion.FromToRotation(mDirection, mNormalzedOffsetDirection);
        IsRunning = true;

    }

    /// <summary>
    /// stop the object from following
    /// </summary>
    public void StopRunning()
    {

        IsRunning = false;

    }

    void PlaceAxis()
    {
        //align axis on model, facing user
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(ReferenceObject.transform.position);
        //determine direction of movement from axis
        axisDirection = gameObject.transform.forward.normalized;
    }

    void Align()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        axisDirection = gameObject.transform.forward.normalized;
    }

    /// <summary>
    /// update the position of the object based on the reference object and configuration
    /// </summary>
    /// <param name="position"></param>
    /// <param name="time"></param>
    protected virtual void UpdatePosition(Vector3 position, float time)
    {
        // update the position
        selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, position, LerpPositionSpeed * time);

    }

    /// <summary>
    /// Animate!
    /// </summary>
    protected virtual void Update()
    {
        if (IsRunning)
        {
            Vector3 newDirection = ReferenceObject.transform.forward;

            // move the object in front of the reference object
            if (KeepInFront)
            {
                if (KeepStartingOffset)
                {
                    newDirection = Vector3.Normalize(mOffsetRotation * ReferenceObject.transform.forward);
                }
            }
            else
            {
                newDirection = mNormalzedOffsetDirection;
                // could we allow drifting?
            }

            Vector3 lerpPosition = ReferenceObject.transform.position + newDirection * mOffsetDistance;
            //find vector projection along axis
            float movement = Vector3.Dot((lerpPosition - selectedObject.transform.position), axisDirection) / axisDirection.magnitude;
            Vector3 axisPosition = axisDirection * movement;

            if(timer > 1f)
            {
                selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, axisPosition, LerpPositionSpeed * Time.deltaTime);
                Align();
            }
            else
            {
                timer += Time.deltaTime;
            }

            //UpdatePosition(lerpPosition, Time.deltaTime);

            
        }
    }
}
