using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Specify name of scene to swap to
/// </summary>
public class SwapScene : MonoBehaviour {

    public void LoadScene(string SceneName) {
        WriteLog.WriteData("Scene: " + SceneName);
        SceneManager.LoadSceneAsync(SceneName);
        
    }

  
}

