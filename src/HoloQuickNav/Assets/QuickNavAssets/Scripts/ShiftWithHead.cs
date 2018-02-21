using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftWithHead : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;
    [Tooltip("Hololens camera")]
    public GameObject cam;

    //variables for tracking user's head movement
    private float currentDistance;
    private float lastDistance;
    private float shiftAmount;

    //options for selecting axis of movement
    private bool rightLeft = false;
    private bool upDown = false;
    private bool forwardBack = false;

    private bool IsRunning = false;

    void OnEnable()
    {
        Align();
        Pause();
        //rotate around y axis so forward arrow is facing user
        Vector3 lookPos = new Vector3(cam.transform.position.x, this.transform.position.y, cam.transform.position.z);
        this.transform.LookAt(lookPos);
        //calculate user's distance from model
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
    }

    public void RightLeft()
    {
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        //adjust visibility of arrows
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveRightArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveLeftArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveUpArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveDownArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveForwardArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveBackArrow").gameObject.SetActive(false);
        //select direction of movement
        rightLeft = true;
        upDown = false;
        forwardBack = false;
        IsRunning = true;
    }

    public void UpDown()
    {
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        //adjust visibility of arrows
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveRightArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveLeftArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveUpArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveDownArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveForwardArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveBackArrow").gameObject.SetActive(false);
        //select direction of movement
        rightLeft = false;
        upDown = true;
        forwardBack = false;
        IsRunning = true;
    }

    public void ForwardBack()
    {
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        //adjust visibility of arrows
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveRightArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveLeftArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveUpArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveDownArrow").gameObject.SetActive(false);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveForwardArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveBackArrow").gameObject.SetActive(true);

        //select direction of movement
        rightLeft = false;
        upDown = false;
        forwardBack = true;
        IsRunning = true;
    }

    public void Pause()
    {
        //disable all motion and deselect any direction of movement
        IsRunning = false;
        rightLeft = false;
        upDown = false;
        forwardBack = false;

        //make all arrows fully visible
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveRightArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveLeftArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveUpArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveDownArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveForwardArrow").gameObject.SetActive(true);
        GameObject.Find("ShiftWithHead").transform.FindChild("MoveBackArrow").gameObject.SetActive(true);

    }

    public void Align()
    {
        //position arrows around model
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
    }
	
	
	void Update () {
        if(IsRunning)
        {
            //calculate current distance and change in user's head position
            currentDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
            shiftAmount = Mathf.Abs(currentDistance - lastDistance) * 0.5f;

            if (currentDistance > lastDistance) //User moves farther away
            {
                if (rightLeft) //move right
                {
                    //calculate direction of movement from forward direction of arrows
                    //will change depending on where user is standing when command is called
                    Vector3 direction = Quaternion.Euler(0, 90, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * shiftAmount;
                }
                else if (upDown) //move up
                { selectedObject.transform.position += new Vector3(0.0f, shiftAmount, 0.0f); }

                else if (forwardBack) //move forward
                {
                    Vector3 direction = gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * shiftAmount * 0.5f;
                }
            }
            else if (currentDistance < lastDistance) //user moves closer
            {
                if (rightLeft) //move left
                {
                    Vector3 direction = Quaternion.Euler(0, -90, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * shiftAmount;
                }
                else if (upDown) //move down
                { selectedObject.transform.position -= new Vector3(0.0f, shiftAmount, 0.0f); }

                else if (forwardBack) //move back
                {
                    Vector3 direction = Quaternion.Euler(0, 180, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * shiftAmount * 0.5f;
                }
            }
            else { } //do nothing if the user hasn't moved their head

            //reposition arrows and update distances
            Align();
            lastDistance = currentDistance;
        }
        
    }

    private void OnDisable()
    {
        Pause();
    }
}
