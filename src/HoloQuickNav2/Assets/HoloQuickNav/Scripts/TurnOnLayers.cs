using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnOnLayers : MonoBehaviour {

    public UnityEvent TurnOn;

	// Use this for initialization
	void OnEnable () {
        TurnOn.Invoke();
	}
	
}
