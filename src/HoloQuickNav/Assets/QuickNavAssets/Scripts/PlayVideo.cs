using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour {

    public MovieTexture movTexture;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0f, 0.1f, 1.5f);
        GetComponent<MeshRenderer>().material.mainTexture = movTexture;
        movTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
