using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftEyeRotation : MonoBehaviour
{

    public GameObject selectedObject;
    public GameObject cam;

    private float currentDistance;
    private float lastDistance;
    private float amount;
    public bool IsRunning = false;
    private Vector3 axis;
    private Vector3 RightMarker;
    private Vector3 NoseMarker;

    // Use this for initialization
    void OnEnable()
    {
        //calculate distance between camera and model
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        RightMarker = selectedObject.transform.FindChild("SubFrame/RightMarker").transform.position;
        NoseMarker = selectedObject.transform.FindChild("SubFrame/NoseMarker").transform.position;
        axis = new Vector3(RightMarker.x - NoseMarker.x, RightMarker.y - NoseMarker.y, RightMarker.z - NoseMarker.z);
        IsRunning = true;
    }

    public void StartRotation()
    {
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        IsRunning = true;
    }

    public void PauseRotation()
    {
        IsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            currentDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);

            if (currentDistance < lastDistance)
            {
                amount = Mathf.Abs(currentDistance - lastDistance) * 500f;
                selectedObject.transform.RotateAround(NoseMarker, axis, amount);
    
            }
            else if (currentDistance > lastDistance)
            {
                amount = Mathf.Abs(currentDistance - lastDistance) * 500f;
                selectedObject.transform.RotateAround(NoseMarker, axis, -amount);
            }
            else
            {
                //do nothing if user hasn't moved thier head
            }

            lastDistance = currentDistance;
        }
        //selectedObject.transform.rotation = Quaternion.AngleAxis(5, axis) * selectedObject.transform.rotation;
        
    }
}
