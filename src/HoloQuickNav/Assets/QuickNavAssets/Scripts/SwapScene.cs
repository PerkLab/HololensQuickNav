using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void LoadTest1 () {
        SceneManager.LoadScene("Test1");
	}

    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene2");
    }
}
