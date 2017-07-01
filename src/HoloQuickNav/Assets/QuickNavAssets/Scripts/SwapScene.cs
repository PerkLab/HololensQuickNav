using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void LoadTest1 () {
        SceneManager.LoadScene("Test1");
	}

    public void LoadMalePlastic()
    {
        SceneManager.LoadScene("MalePlasticPhantom");
    }
}
