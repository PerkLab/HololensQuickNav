using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour {

    public GameObject cam;
    public GameObject selectedObject;

    private Vector3 lastDirectionCam;
    private Vector3 currentDirectionCam;
    private float currentDistance;
    private float lastDistance;
    private float amount;

    private bool Copy1 = false;
    private bool Copy2 = false;

    private bool IsRunning = false;


    private void OnEnable()
    {
        CopyRS();
    }

    // Use this for initialization
	public void CopyRS()
    {
        lastDirectionCam = cam.transform.forward.normalized;
        Copy1 = true;
        Copy2 = false;
        IsRunning = true;
    }

    public void CopyA()
    {
        lastDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);
        Copy1 = false;
        Copy2 = true;
        IsRunning = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(IsRunning)
        {
            if(Copy1)
            {
                currentDirectionCam = cam.transform.forward.normalized;

                if (currentDirectionCam.x > lastDirectionCam.x)
                {
                    //user's right direction is +x, model's right is -x
                    //to rotate right, rotate in positive direction about z axis
                    amount = Mathf.Abs(currentDirectionCam.x - lastDirectionCam.x) * 80f;
                    selectedObject.transform.Rotate(new Vector3(0f, 0f, amount));
                }
                else if (currentDirectionCam.x < lastDirectionCam.x)
                {
                    //user's left direction is -x, model's left is +x
                    //to rotate left, rotate in negative direction about z axis
                    amount = Mathf.Abs(currentDirectionCam.x - lastDirectionCam.x) * 80f;
                    selectedObject.transform.Rotate(new Vector3(0f, 0f, -amount));
                }


                if (currentDirectionCam.y > lastDirectionCam.y) //chin up
                {
                    //user's up direction is +y, model's up is +y
                    //to rotate up, rotate in positive direction about x axis
                    amount = Mathf.Abs(currentDirectionCam.y - lastDirectionCam.y) * 80f;
                    selectedObject.transform.Rotate(new Vector3(amount, 0f, 0f));
                }
                else if (currentDirectionCam.y < lastDirectionCam.y)
                {
                    //user's left direction is -x, model's left is +x
                    //to rotate left, rotate in negative direction about z axis
                    amount = Mathf.Abs(currentDirectionCam.y - lastDirectionCam.y) * 80f;
                    selectedObject.transform.Rotate(new Vector3(-amount, 0f, 0f));
                }
                else
                {
                    //do nothing 
                }

                //update direction
                lastDirectionCam = currentDirectionCam;

            }
            
            if(Copy2)
            {
                //calculate the distance from the user to the empty game object
                currentDistance = Vector3.Distance(cam.transform.position, selectedObject.transform.position);

                if (currentDistance < lastDistance) //if user is closer to the model
                {
                    amount = Mathf.Abs(currentDistance - lastDistance)*50;
                    selectedObject.transform.Rotate(0f, amount, 0f);
                }
                else//(currentDistance > lastDistance) //if user is farther from the model
                {
                    
                    amount = Mathf.Abs(currentDistance - lastDistance)*50;
                    selectedObject.transform.Rotate(0f, -amount, 0f);
                }

                //update the distances
                lastDistance = currentDistance;
            }
            
        }
        
	}
}
