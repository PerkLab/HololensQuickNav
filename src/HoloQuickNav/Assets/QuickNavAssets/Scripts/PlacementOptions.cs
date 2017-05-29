using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementOptions : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject cam;
	
    // Use this for initialization
	void Start () {
        gameObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
        gameObject.transform.LookAt(cam.transform);
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.LookAt(cam.transform);
    }

    public void Option1()
    {
        selectedObject.transform.rotation = gameObject.transform.FindChild("Placement1").transform.rotation;
    }

    public void Option2()
    {
        selectedObject.transform.rotation = gameObject.transform.FindChild("Placement2").transform.rotation;
    }

    public void Option3()
    {
        selectedObject.transform.rotation = gameObject.transform.FindChild("Placement3").transform.rotation;
    }

    public void Option4()
    {
        selectedObject.transform.rotation = gameObject.transform.FindChild("Placement4").transform.rotation;
    }
}
