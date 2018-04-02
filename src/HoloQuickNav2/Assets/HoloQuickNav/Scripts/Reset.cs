using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {

    [Tooltip("Object you wish to move")]
    public GameObject selectedObject;

	void OnEnable () {
        //move object back to it's original position 
        selectedObject.transform.position = new Vector3(0f, 0.1f, 1.5f);
    }
}
