using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthWithHead : MonoBehaviour
{

    [Tooltip("Hololens Camera")]
    public GameObject cam;
    [Tooltip("Object to move")]
    public GameObject selectedObject;

    private Vector3 axisDirection;
    private float currentDistance;
    private float lastDistance;
    //amount to shift by
    private float amount;
    private bool IsRunning = false;

    // Use this for initialization
    void OnEnable()
    {
        //align axis on model, facing user
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(cam.transform.position);
        //determine direction of movement from axis
        axisDirection = gameObject.transform.forward.normalized;
        //calculate distance between camera and model
        lastDistance = Vector3.Distance(cam.transform.position, transform.position);
        IsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            //calculate current distance between camera and model
            currentDistance = Vector3.Distance(cam.transform.position, transform.position);

            //if user has moved closer, shift object in positive direction
            if (currentDistance < lastDistance) 
            {
                amount = Mathf.Abs(currentDistance - lastDistance) * 0.8f; //0.5
                selectedObject.transform.position += axisDirection * (-amount);
            }
            //if user has moved farther, shift object in negative direction
            else if (currentDistance > lastDistance)
            {
                amount = Mathf.Abs(currentDistance - lastDistance) * 0.8f;
                selectedObject.transform.position += axisDirection * amount;
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
