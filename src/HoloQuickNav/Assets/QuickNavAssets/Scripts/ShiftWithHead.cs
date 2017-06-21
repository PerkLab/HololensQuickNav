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
    private float amount;

    //options for selecting axis of movement
    private bool rightLeft = false;
    private bool upDown = false;
    private bool forwardBack = false;

    private bool IsRunning = false;

    void OnEnable()
    {
        Align();
        //rotate around y axis so forward arrow is facing user
        Vector3 lookPos = new Vector3(cam.transform.position.x, this.transform.position.y, cam.transform.position.z);
        this.transform.LookAt(lookPos);
        //calculate user's distance from model
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
    }

    public void RightLeft()
    {
        //adjust visibility of arrows
        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        //select direction of movement
        rightLeft = true;
        upDown = false;
        forwardBack = false;
        IsRunning = true;
    }

    public void UpDown()
    {
        //adjust visibility of arrows
        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        //select direction of movement
        rightLeft = false;
        upDown = true;
        forwardBack = false;
        IsRunning = true;
    }

    public void ForwardBack()
    {
        //adjust visibility of arrows
        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.1f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
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
        GameObject LRArrows = GameObject.Find("ShiftWithHead");
        LRArrows.transform.FindChild("MoveRightArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject UDArrows = GameObject.Find("ShiftWithHead");
        UDArrows.transform.FindChild("MoveUpArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);
        GameObject FBArrows = GameObject.Find("ShiftWithHead");
        FBArrows.transform.FindChild("MoveForwardArrow").transform.FindChild("Box").GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Metallic", 0.5f);

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
            amount = Mathf.Abs(currentDistance - lastDistance);

            if (currentDistance > lastDistance) //User moves farther away
            {
                if (rightLeft) //move right
                {
                    Vector3 direction = Quaternion.Euler(0, 90, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * amount;
                }
                else if (upDown) //move up
                { selectedObject.transform.position += new Vector3(0.0f, amount, 0.0f); }

                else if (forwardBack) //move forward
                {
                    Vector3 direction = gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * amount * 0.5f;
                }
            }
            else if (currentDistance < lastDistance) //user moves closer
            {
                if (rightLeft) //move left
                {
                    Vector3 direction = Quaternion.Euler(0, -90, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * amount;
                }
                else if (upDown) //move down
                { selectedObject.transform.position -= new Vector3(0.0f, amount, 0.0f); }

                else if (forwardBack) //move back
                {
                    Vector3 direction = Quaternion.Euler(0, 180, 0) * gameObject.transform.forward.normalized;
                    selectedObject.transform.position += direction * amount * 0.5f;
                }
            }
            else { } //do nothing if the user hasn't moved their head

            //reposition arrows and update distances
            Align();
            lastDistance = currentDistance;
        }
        
    }
}
