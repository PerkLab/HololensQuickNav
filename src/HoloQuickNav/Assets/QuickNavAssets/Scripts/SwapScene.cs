using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void LoadCLPhantom () {
        //WriteLog.WriteData("Scene: CL Phantom");
        SceneManager.LoadSceneAsync("CLPhantom");
        
    }

    public void LoadMalePlastic()
    {
        //WriteLog.WriteData("Scene: Male Plastic");
        SceneManager.LoadScene("MalePlastic");
        
    }

    public void LoadInstructions()
    {
        //WriteLog.WriteData("Open Instructions");
        SceneManager.LoadSceneAsync("Instructions", LoadSceneMode.Additive);
        CurrentSceneOnOff(false);
    }

    public void UnloadInstructions()
    {
        //WriteLog.WriteData("Close Instructions");
        CurrentSceneOnOff(true);
        SceneManager.UnloadSceneAsync("Instructions");
    }


    void CurrentSceneOnOff(bool visible)
    {
        //clear everything from current scene when instructions are opened
        //start at "done" command

        GameObject.Find("Head").transform.FindChild("Model").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.FindChild("Done").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.FindChild("CommandName").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.FindChild("HelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("InputManager").transform.FindChild("VoiceInput").gameObject.SetActive(visible);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorDot").gameObject.SetActive(visible);
        GameObject.Find("InteractiveMeshCursor").transform.FindChild("CursorRing").gameObject.SetActive(visible);

    }


}
