using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void RotateRight () {
        gameObject.transform.Rotate(new Vector3(0.0f, -2.0f, 0.0f));
	}

    public void RotateLeft()
    {
        gameObject.transform.Rotate(new Vector3(0.0f, 2.0f, 0.0f));
    }
}
