using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnOnLayers : MonoBehaviour {


	// Use this for initialization
	void OnEnable () {

        //turn on all layers
        foreach(Transform child in GameObject.Find("WorldAnchor/Model").transform.Find("Layers").GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }
	}
	
}
