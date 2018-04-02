using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementOptions : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;
    [Tooltip("Hololens camera")]
    public GameObject cam;
	
	void OnEnable () {
        //align models of options with actual model
        gameObject.transform.position = new Vector3(selectedObject.transform.Find("Model").transform.position.x, 
                                                    selectedObject.transform.Find("Model").transform.position.y, 
                                                    selectedObject.transform.Find("Model").transform.position.z);
        //turn models to face user
        gameObject.transform.LookAt(cam.transform);
    }

	void Update () {
        //always face user
        gameObject.transform.LookAt(cam.transform);
    }

    //when an option is selected, update model's rotation to match the model selected
    public void Option1()
    {
        selectedObject.transform.rotation = gameObject.transform.Find("Placement1").transform.rotation;
    }

    public void Option2()
    {
        selectedObject.transform.rotation = gameObject.transform.Find("Placement2").transform.rotation;
    }

    public void Option3()
    {
        selectedObject.transform.rotation = gameObject.transform.Find("Placement3").transform.rotation;
    }

    public void Option4()
    {
        selectedObject.transform.rotation = gameObject.transform.Find("Placement4").transform.rotation;
    }
}
