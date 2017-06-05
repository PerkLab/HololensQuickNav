using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthTest2 : MonoBehaviour {

    public GameObject cam;
    public GameObject selectedObject;

    private Vector3 axisDirection;

    private float currentDistance;
    private float lastDistance;
    private float amount;
    public bool IsRunning = false;

    // Use this for initialization
    void OnEnable () {
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
	void Update () {
        if(IsRunning)
        {
            currentDistance = Vector3.Distance(cam.transform.position, transform.position);

            if (currentDistance < lastDistance)
            {
                amount = Mathf.Abs(currentDistance - lastDistance) * 0.5f;
                selectedObject.transform.position += axisDirection * amount;
            }
            else if (currentDistance > lastDistance)
            {
                amount = Mathf.Abs(currentDistance - lastDistance) * 0.5f;
                selectedObject.transform.position += axisDirection * (-amount);
            }
            else
            {
                //do nothing if user hasn't moved thier head
            }

            lastDistance = currentDistance;

            Align();
        }
        
	}

    void Align()
    {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        //axisDirection = gameObject.transform.forward.normalized;
    }
}
