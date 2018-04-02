using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePos : MonoBehaviour {

    private bool timerEnabled = false;
    private float timer;
    public GameObject cam;

	public void SavePosition()
    {
        WriteLog.SavePosition();
        Align();
        DisplayTextSave();
    }

    void DisplayTextSave()
    {
        gameObject.GetComponent<TextMesh>().text = "Position Saved";
        timer = 0f;
        timerEnabled = true;
    }

    public void ResetID()
    {
        WriteLog.WriteData("User ID Reset");
        Align();
        DisplayTextReset();
    }

    void DisplayTextReset()
    {
        gameObject.GetComponent<TextMesh>().text = "User ID Reset";
        timer = 0f;
        timerEnabled = true;
    }

    public void CheckID()
    {
        Align();
        DisplayTextCheck();
    }

    void DisplayTextCheck()
    {
        gameObject.GetComponent<TextMesh>().text = "User ID: " + WriteLog.ID;
        timer = 0f;
        timerEnabled = true;
    }

    private void Update()
    {
        if(timerEnabled)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                gameObject.GetComponent<TextMesh>().text = "";
                timerEnabled = false;
            }
        }
        
    }

    private void Align()
    {
        gameObject.transform.position = cam.transform.position + cam.transform.forward * 1f;
    }

}
