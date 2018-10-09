using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnOnLayers : MonoBehaviour {

    public UnityEvent TurnOn;

	// Use this for initialization
	void OnEnable () {
        TurnOn.Invoke();
        foreach (Transform child in GameObject.Find("Model").transform.Find("Layers").transform)
        {
            child.gameObject.SetActive(false);
        }
        GameObject.Find("Model").transform.Find("Layers").transform.GetChild(0).gameObject.SetActive(true);
    }

}
