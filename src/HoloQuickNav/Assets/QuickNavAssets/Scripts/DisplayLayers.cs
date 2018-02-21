using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLayers : MonoBehaviour {

    private List<GameObject> layers;
    private int numberOfLayers;

	// Use this for initialization
	void Start () {
        
        foreach(Transform child in GameObject.Find("Model").transform.FindChild("Layers").transform)
        {
            layers.Add(child.gameObject);
        }
        
	}

    private void AddButton(GameObject layer)
    {
        //button text = layer.name;

    }

}
