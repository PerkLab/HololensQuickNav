using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithHead : MonoBehaviour {

    [Tooltip("The game object the object will follow")]
    public GameObject ReferenceObject;

    [Tooltip("The object to move")]
    public GameObject selectedObject;

    [Tooltip("Use short offset distance")]
    public bool StayClose = false;

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

    // this object's normalized direction
    private Vector3 mNormalzedOffsetDirection;

    // is command updating every frame
    bool IsRunning = false;

    /// <summary>
    /// set the reference object if not set already
    /// </summary>
    private void OnEnable()
    {
        StartRunning();
    }

    // start the object following the reference object
    public void StartRunning()
    {
        mOffsetDirection = selectedObject.transform.position - ReferenceObject.transform.position;
        mOffsetDistance = mOffsetDirection.magnitude;
        if(StayClose)
        {
            mOffsetDistance = .9f;
        }
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
            }

            // update the object's position using specified lerpSpeed for movement
            Vector3 lerpPosition = ReferenceObject.transform.position + newDirection * mOffsetDistance;
            selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, lerpPosition, LerpPositionSpeed * Time.deltaTime);
        }
        
    }
}
