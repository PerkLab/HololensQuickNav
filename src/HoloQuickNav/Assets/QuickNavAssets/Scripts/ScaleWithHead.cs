using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithHead : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;
    [Tooltip("Hololens camera")]
    public GameObject cam;

    //variables for tracking movements in the user's head
    private float currentDistance;
    private float lastDistance;
    private float amount;

	void OnEnable () {
        //position the empty game object around the selected object
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        //calculate the distance from the user to the empty game object
        lastDistance = Vector3.Distance(cam.transform.position, transform.position);
    }
	
	void Update () {
        //calculate the distance from the user to the empty game object
        currentDistance = Vector3.Distance(cam.transform.position, transform.position);

        if(currentDistance < lastDistance) //if user is closer to the model
        {
            //scale the model to a small size
            amount = Mathf.Abs(currentDistance - lastDistance);
            selectedObject.transform.localScale += new Vector3(-amount, -amount, -amount);
        }
        else if(currentDistance > lastDistance) //if user is farther from the model
        {
            //scale the model to a larger size
            amount = Mathf.Abs(currentDistance - lastDistance);
            selectedObject.transform.localScale += new Vector3(amount, amount, amount);
        }
        else
        {
            //do nothing if user hasn't moved thier head
        }
        //update the distances
        lastDistance = currentDistance;
        
    }
}
