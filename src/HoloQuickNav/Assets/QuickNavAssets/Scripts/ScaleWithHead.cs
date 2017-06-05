using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithHead : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject cam;
    private float currentDistance;
    private float lastDistance;
    private float amount;

	// Use this for initialization
	void OnEnable () {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        lastDistance = Vector3.Distance(cam.transform.position, transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        currentDistance = Vector3.Distance(cam.transform.position, transform.position);

        if(currentDistance < lastDistance)
        {
            amount = Mathf.Abs(currentDistance - lastDistance);
            selectedObject.transform.localScale += new Vector3(-amount, -amount, -amount);
        }
        else if(currentDistance > lastDistance)
        {
            amount = Mathf.Abs(currentDistance - lastDistance);
            selectedObject.transform.localScale += new Vector3(amount, amount, amount);
        }
        else
        {
            //do nothing if user hasn't moved thier head
        }

        lastDistance = currentDistance;
        
    }
}
