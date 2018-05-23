using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SwapScene : MonoBehaviour {

    
    public static string CommandName;
    

    public void LoadScene(string SceneName) {
        WriteLog.WriteData("Scene: " + SceneName);
        SceneManager.LoadSceneAsync(SceneName);
        
    }

  
}

