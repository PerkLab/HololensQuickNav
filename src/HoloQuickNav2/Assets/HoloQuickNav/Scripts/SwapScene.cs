using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SwapScene : MonoBehaviour {

    
    public static string CommandName;
    /*
    public UnityEvent ThreePointRegBegin;
    public UnityEvent ThreePointRegDone;
    public UnityEvent Reset;
    */
    

    public void LoadScene(string SceneName) {
        WriteLog.WriteData("Scene: " + SceneName);
        SceneManager.LoadSceneAsync(SceneName);
        
    }

    /*
    public void LoadMalePlastic()
    {
        WriteLog.WriteData("Scene: Male Plastic");
        SceneManager.LoadScene("MalePlastic");
        
    }

    public void LoadFemalePlastic()
    {
        WriteLog.WriteData("Scene: Female Plastic");
        SceneManager.LoadScene("FemalePlastic");

    }*/


    /*
    public void LoadInstructions(string Command)
    {
        WriteLog.WriteData("Open Instructions");
        if (Command == "Done")
        {
            DoneOnOff(false);
            CommandName = "Done";
        }
        else if (Command == "Shift")
        {
            ShiftOnOff(false);
            CommandName = "Shift";
        }
        else if (Command == "Rotate")
        {
            RotateOnOff(false);
            CommandName = "Rotate";
        }
        else if (Command == "Move")
        {
            MoveOnOff(false);
            CommandName = "Move";
        }
        else if (Command == "3Point")
        {
            ThreePointOnOff(false);
            DoneOnOff(false);
            CommandName = "3Point";
        }
        SceneManager.LoadSceneAsync("Instructions", LoadSceneMode.Additive);
    }

    public void UnloadInstructions()
    {
        WriteLog.WriteData("Close Instructions");
        if(CommandName == "Done")
        {
            DoneOnOff(true);
        }
        else if (CommandName == "Shift")
        {
            ShiftOnOff(true);
        }
        else if (CommandName == "Rotate")
        {
            RotateOnOff(true);
        }
        else if (CommandName == "Move")
        {
            MoveOnOff(true);
        }
        else if (CommandName == "3Point")
        {
            DoneOnOff(true);
            ThreePointOnOff(true);
        }
        SceneManager.UnloadSceneAsync("Instructions");
    }


    void DoneOnOff(bool visible)
    {
        //clear everything from current scene when instructions are opened
        //start from 'done' command

        GameObject.Find("Head").transform.Find("Model").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("Done").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("CommandName").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("DoneCommandName").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("HelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("InputManager").transform.Find("VoiceInput").gameObject.SetActive(visible);

        WriteLog.WriteData("done");

        if (CommandName == "3Point")
        {
            WriteLog.WriteData("invoke pt");
            ThreePointRegBegin.Invoke();
        }
    }

    void ShiftOnOff(bool visible)
    {
        //clear everything from current scene when instructions are opened
        //start from 'shift' command

        GameObject.Find("Head").transform.Find("Model").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("ShiftWithHead").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("CommandName").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("PauseHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("InputManager").transform.Find("VoiceInput").gameObject.SetActive(visible);
    }

    void RotateOnOff(bool visible)
    {
        //clear everything from current scene when instructions are opened
        //start from 'rotate' command

        GameObject.Find("Head").transform.Find("Model").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.Find("AxisA").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.Find("AxisR").gameObject.SetActive(visible);
        GameObject.Find("Head").transform.Find("AxisS").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("Rotate3Axis").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("CommandName").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("PauseHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("InputManager").transform.Find("VoiceInput").gameObject.SetActive(visible);
    }

    void MoveOnOff(bool visible)
    {
        //clear everything from current scene when instructions are opened
        //start from 'move' command

        GameObject.Find("Head").transform.Find("Model").gameObject.SetActive(visible);
        GameObject.Find("Controls").transform.Find("MoveFull").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("CommandName").gameObject.SetActive(visible);
        GameObject.Find("CommandText").transform.Find("MoveHelpAndMenu").gameObject.SetActive(visible);
        GameObject.Find("InputManager").transform.Find("VoiceInput").gameObject.SetActive(visible);
    }

    void ThreePointOnOff(bool visible)
    {
        if (visible)
        {
            //reset model
            Reset.Invoke();
        }
        else // (!visible)
        {
            //call done command from 3 point
            ThreePointRegDone.Invoke();
            Reset.Invoke();
        }
    }
    */
}

