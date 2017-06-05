using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {

    public GameObject selectedObject;

	// Use this for initialization
	void OnEnable () {
        selectedObject.transform.position = new Vector3(0f, .1f, 1.5f);
	}
}
