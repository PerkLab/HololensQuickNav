using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.Find("LoadedModels"))
        {
            Object.Destroy(GameObject.Find("LoadedModels"));
        }

    }

}
